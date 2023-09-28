using System;
using Pulumi;
using Rds = Pulumi.Aws.Rds;
using Ec2 = Pulumi.Aws.Ec2;

public class Database {
    public static (Rds.Instance, Ec2.SecurityGroup)  CreateRds(Output<string> vpcId, Output<Ec2.GetSubnetsResult> subnets)
    {

        var stackName = Deployment.Instance.StackName;
    
        var sgDatabase = new Ec2.SecurityGroup($"database-sg-{stackName}-", new Ec2.SecurityGroupArgs
        {
            VpcId = vpcId,
            Ingress =
            {
                new Ec2.Inputs.SecurityGroupIngressArgs
                {
                    Protocol = "tcp",
                    FromPort = 3306,
                    ToPort = 3306,
                    CidrBlocks = { "0.0.0.0/0" }
                },
            },
            Egress =
            {
                new Ec2.Inputs.SecurityGroupEgressArgs
                {
                    Protocol = "-1",
                    FromPort = 0,
                    ToPort = 0,
                    CidrBlocks = { "0.0.0.0/0" }
                }
            },
        });

        var instance = new Rds.Instance($"database-{stackName}-", 
            new Rds.InstanceArgs()
            {
                AllocatedStorage = 10,
                DbName = "pucheta",
                Engine = "mariadb",
                EngineVersion = "10.11",
                InstanceClass = Rds.InstanceType.T3_Medium,
                ParameterGroupName = "pucheta-parameters",
                Username = "puser",
                Password = "kj123hefv98!$1413",
                SkipFinalSnapshot = true,
                PubliclyAccessible = true,
                VpcSecurityGroupIds = new InputList<string>() { vpcId },
            }
        );

        return (instance, sgDatabase) ;
    }
}