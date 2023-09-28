using System;
using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using Pulumi;
using Pulumi.Aws.Connect;
using Docker = Pulumi.Docker;
using Ec2 = Pulumi.Aws.Ec2;
using Ecs = Pulumi.Aws.Ecs;
using Ecr = Pulumi.Aws.Ecr;
using Elb = Pulumi.Aws.LB;
using Iam = Pulumi.Aws.Iam;

class PuchetaStack : Stack {

    public PuchetaStack() {
        var stackName = Deployment.Instance.StackName;

        // Read back the default VPC and public subnets, which we will use.
        var vpcId = Ec2.GetVpc.Invoke(new Ec2.GetVpcInvokeArgs { Default = true })
            .Apply(vpc => vpc.Id);
        
        var subnets = Ec2.GetSubnets.Invoke(new Ec2.GetSubnetsInvokeArgs
        {
            Filters = new []
            {
                new Ec2.Inputs.GetSubnetsFilterInputArgs
                {
                    Name = $"vpc-id",
                    Values = new[] { vpcId}
                }
            }
        });
        
        var (database, sgDatabase)  = Database.CreateRds(vpcId, subnets);

        var subnetIds = subnets.Apply(s => s.Ids);

        // Create a SecurityGroup that permits HTTP ingress and unrestricted egress.
        var webSg = new Ec2.SecurityGroup($"web-sg-{stackName}", new Ec2.SecurityGroupArgs
        {
            VpcId = vpcId,
            Egress =
            {
                new Ec2.Inputs.SecurityGroupEgressArgs
                {
                    Protocol = "-1",
                    FromPort = 0,
                    ToPort = 0,
                    CidrBlocks = {"0.0.0.0/0"}
                }
            },
            Ingress =
            {
                new Ec2.Inputs.SecurityGroupIngressArgs
                {
                    Protocol = "tcp",
                    FromPort = 80,
                    ToPort = 80,
                    CidrBlocks = {"0.0.0.0/0"}
                }
            },
        });

        // Create an ECS cluster to run a container-based service.
        var cluster = new Ecs.Cluster($"app-cluster-{stackName}");

        var rolePolicyJson = JsonSerializer.Serialize(new
        {
            Version = "2008-10-17",
            Statement = new[]
            {
                new
                {
                    Sid = "",
                    Effect = "Allow",
                    Principal = new
                    {
                        Service = "ecs-tasks.amazonaws.com"
                    },
                    Action = "sts:AssumeRole"
                }
            }
        });

        // Create an IAM role that can be used by our service's task.
        var taskExecRole = new Iam.Role($"task-exec-role-{stackName}", new Iam.RoleArgs
        {
            AssumeRolePolicy = rolePolicyJson
        });

        var taskExecAttach = new Iam.RolePolicyAttachment("task-exec-policy", new Iam.RolePolicyAttachmentArgs
        {
            Role = taskExecRole.Name,
            PolicyArn = "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
        });

        // Create a load balancer to listen for HTTP traffic on port 80.
        var webLb = new Elb.LoadBalancer($"web-lb-{stackName}", new Elb.LoadBalancerArgs
        {
            Subnets = subnetIds,
            SecurityGroups = { webSg.Id }
        });
        
        var webTg = new Elb.TargetGroup($"web-tg-{stackName}", new Elb.TargetGroupArgs
        {
            Port = 80,
            Protocol = "HTTP",
            TargetType = "ip",
            VpcId = vpcId
        });
        
        var webListener = new Elb.Listener($"web-listener-{stackName}", new Elb.ListenerArgs
        {
            LoadBalancerArn = webLb.Arn,
            Port = 80,
            DefaultActions =
            {
                new Elb.Inputs.ListenerDefaultActionArgs
                {
                    Type = "forward",
                    TargetGroupArn = webTg.Arn,
                }
            }
        });

        // Create a private ECR registry and build and publish our app's container image to it.
        var appRepo = new Ecr.Repository($"app-repo-{stackName}");

        var appRepoCredentials = Ecr.GetCredentials
            .Invoke(new Ecr.GetCredentialsInvokeArgs { RegistryId = appRepo.RegistryId })
            .Apply(credentials =>
            {
                var data = Convert.FromBase64String(credentials.AuthorizationToken);
                return Encoding.UTF8.GetString(data).Split(":").ToImmutableArray();
            });
        
        var image = new Docker.Image($"app-pucheta-{stackName}", new()
        {
            Build = new Docker.Inputs.DockerBuildArgs
            {
                Platform = "linux/amd64",
                Context = "../src",
                Dockerfile = "../src/Dockerfile",
            },
            ImageName = appRepo.RepositoryUrl,
            SkipPush = true,


        });
        
        // Spin up a load balanced service running our container image.
        var appTask = new Ecs.TaskDefinition($"app-task-{stackName}", new Ecs.TaskDefinitionArgs
        {
            Family = "fargate-task-definition",
            Cpu = "256",
            Memory = "512",
            NetworkMode = "awsvpc",
            RequiresCompatibilities = { "FARGATE" },
            ExecutionRoleArn = taskExecRole.Arn,
            ContainerDefinitions = image.ImageName.Apply(imageName => JsonSerializer.Serialize(new[]
            {
                new
                {
                    name = $"pucheta-web-cd-{stackName}",
                    image = imageName,
                    portMappings = new[]
                    {
                        new
                        {
                            containerPort = 80,
                            hostPort = 80,
                            protocol = "tcp"
                        }
                    }
                }
            }))
        });

        var appSvc = new Ecs.Service($"app-svc-{stackName}", new Ecs.ServiceArgs
        {
            Cluster = cluster.Arn,
            DesiredCount = 3,
            LaunchType = "FARGATE",
            TaskDefinition = appTask.Arn,

            NetworkConfiguration = new Ecs.Inputs.ServiceNetworkConfigurationArgs
            {
                AssignPublicIp = true,
                Subnets = subnetIds,
                SecurityGroups = { webSg.Id, sgDatabase.Id }
            },
            LoadBalancers =
            {
                new Ecs.Inputs.ServiceLoadBalancerArgs
                {
                    TargetGroupArn = webTg.Arn,
                    ContainerName = $"pucheta-web-cd-{stackName}",
                    ContainerPort = 80
                }
            }
        }, new CustomResourceOptions { DependsOn = { webListener } });

        

        this.RdsUrl = database.Endpoint; 
        this.Url = Output.Format($"http://{webLb.DnsName}");
    }
    
    [Output] 
    public Output<string> Url { get; set; }
    [Output] 
    public Output<string> RdsUrl { get; set; }

}