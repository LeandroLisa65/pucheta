using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Gestion.EF.Models
{
    public partial class iotdbContext : DbContext
    {
        private string connectionString;

        public iotdbContext() : base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            //connectionString = Security.Decrypt(configuration.GetConnectionString("MySql_db").ToString());
            connectionString = configuration.GetConnectionString("MySql_db");


        }

        public iotdbContext(DbContextOptions<iotdbContext> options) : base(options)  { }

        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<UsuariosLog> UsuariosLog { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesUsuarios> RolesUsuarios { get; set; }
        public virtual DbSet<Acciones> Acciones { get; set; }
        public virtual DbSet<AccionesRoles> AccionesRoles { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<UsuariosDireccion> UsuariosDireccion { get; set; }
        public virtual DbSet<UsuariosDispositivo> UsuariosDispositivo { get; set; }
        public virtual DbSet<Dispositivos> Dispositivos { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<Localidades> Localidades { get; set; }

        public virtual DbSet<Proyecto> Proyecto { get; set; }
        public virtual DbSet<Proyecto_Ubicaciones> Proyecto_Ubicaciones { get; set; }

        public virtual DbSet<ParteDiario> ParteDiario { get; set; }
        public virtual DbSet<ParteDiario_Actividades> ParteDiario_Actividades { get; set; }
        public virtual DbSet<ParteDiario_Actividades_Fotos> ParteDiario_Actividades_Fotos { get; set; }
        public virtual DbSet<ParteDiario_Contratista> ParteDiario_Contratista { get; set; }
        public virtual DbSet<ParteDiario_Sol_ObrasAdic> ParteDiario_Sol_ObrasAdic { get; set; }

        public virtual DbSet<Planificacion_Actividades> Planificacion_Actividades { get; set; }
        public virtual DbSet<Planificacion_Actividades_Rubro> Planificacion_Actividades_Rubro { get; set; }
        public virtual DbSet<Planificacion_Actividades_Tareas> Planificacion_Actividades_Tareas { get; set; }
        public virtual DbSet<Planificacion_Proyecto_Actividades> Planificacion_Proyecto_Actividades { get; set; }
        public virtual DbSet<Planificacion_Proyecto_Actividades_Log> Planificacion_Proyecto_Actividades_Log { get; set; }
        public virtual DbSet<Contratistas> Contratistas { get; set; }
        public virtual DbSet<ParteDiario_Actividades_Contratista> ParteDiario_Actividades_Contratista { get; set; }
        public virtual DbSet<ParteDiario_Incidentes> ParteDiario_Incidentes { get; set; }
        public virtual DbSet<Incidentes> Incidentes { set; get;}
        public virtual DbSet<IncidentesTipo> IncidentesTipo { set; get;}
        public virtual DbSet<ParteDiario_Asistencia> ParteDiario_Asistencia { set; get; }
        public virtual DbSet<Incidentes_Historial> Incidentes_Historial { set; get; }
        public virtual DbSet<IncidentesHistorial_Detalle> IncidentesHistorial_Detalle { set; get; }

        public virtual DbSet<Archivos_Adjuntos> Archivos_Adjuntos { set; get; }
        public virtual DbSet<Archivos_Adjuntos_Relacion> Archivos_Adjuntos_Relacion { set; get; }
        public virtual DbSet<Proyecto_Contratista> Proyecto_Contratista { set; get; }

        public virtual DbSet<Calidad_Items> Calidad_Items { set; get; }
        public virtual DbSet<Calidad_Actividades_Valoracion> Calidad_Actividades_Valoracion { set; get; }

        public virtual DbSet<Planificacion_Actividades_Calidad_Items> Planificacion_Actividades_Calidad_Items { set; get; }
        public virtual DbSet<Informe_Actividad_Vencida> Informes_Actividades_Vencidas { get; set; }
        public virtual DbSet<PlanProyAct_Dependencia> PlanProyAct_Dependencias { get; set; }
        public virtual DbSet<ContrCertificado> Certificados_Contratistas { get; set; }
        public virtual DbSet<ContrCert_PDActContr> CertContrs_PDActContrs { get; set; }
        public virtual DbSet<CertContr_PlanProyAct> CertContrs_PlanProyActs { get; set; }
        public virtual DbSet<ProyCertificado> ProyCertificados { get; set; }
        public virtual DbSet<ProyCert_PlanProyAct> ProyCert_PlanProyActs { get; set; }
        public virtual DbSet<ProyCert_PDActContr> ProyCert_PDActContrs { get; set; }
        public virtual DbSet<ProyCert_PDActContr_Adicional> ProyCert_PDActContr_Adicionales { get; set; }
        public virtual DbSet<Notificacion> Notificaciones { get; set; }
        public virtual DbSet<PartidaPresupuestaria> PartidasPresupuestarias { get; set; }
        public virtual DbSet<ProyCert_Contratista> ProyCert_Contratistas { get; set; }
        public virtual DbSet<ProyCert_Empleado> ProyCert_Empleados { get; set; }
        public virtual DbSet<ProcesoAutomatico> ProcesosAutomaticos { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<ProyContr_Descuento> ProyContr_Descuentos { get; set; }
        public virtual DbSet<NotaPedido> NotaPedido { get; set; }
        public virtual DbSet<NotaPedido_Detalle> NotaPedido_Detalle { get; set; }

        public virtual DbSet<Indices> Indices { get; set; }
        public virtual DbSet<Indice_Valores> Indice_Valores { get; set; }

        public virtual DbSet<Certificados> Certificados { get; set; }
        public virtual DbSet<Certificados_detalle_planificado> certificados_detalle_planificado { get; set; }
        public virtual DbSet<Certificados_detalle_adicional> certificados_detalle_adicional { get; set; }
        public virtual DbSet<Certificados_detalle_liquidacion> certificados_detalle_liquidacion { get; set; }
        public virtual DbSet<Certificados_detalle_aplicaciones_pagos> certificados_aplicacion_pagos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                System.Console.WriteLine("CADENA DE CONEXION");
                System.Console.WriteLine(connectionString);
                var dbConnection = new MySqlConnection(connectionString);
                optionsBuilder.UseMySql(dbConnection, ServerVersion.AutoDetect(connectionString));
                //optionsBuilder.UseMySql("server=127.0.0.1; User Id=root; pwd=Laliho*65; database=pucheta; Persist Security Info=True");
                //server=192.168.0.224; User Id=root; pwd=achu0303; database=puchetadb; Persist Security Info=True
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menus>(entity =>
            {
                entity.ToTable("menus");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Activo).HasColumnType("tinyint(4)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(255)");

                entity.Property(e => e.Icon).HasColumnType("varchar(45)");

                entity.Property(e => e.IdMenuPadre)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Nombre).HasColumnType("varchar(45)");

                entity.Property(e => e.Orden).HasColumnType("int(11)");

                entity.Property(e => e.Path).HasColumnType("varchar(255)");

                entity.Property(e => e.Tipo).HasColumnType("varchar(1)");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Activo).HasColumnType("tinyint(4)");

                entity.Property(e => e.Apellido).HasColumnType("varchar(45)").HasField("_apellido");

                entity.Property(e => e.Avatar).HasColumnType("varchar(250)");

                entity.Property(e => e.Clave).HasColumnType("varchar(250)");

                entity.Property(e => e.Email).HasColumnType("varchar(50)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Grupo)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.NUsuario)
                    .HasColumnName("nUsuario")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(45)").HasField("_nombre");
                
                entity.Property(e => e.Rsid).HasColumnType("varchar(250)");

                entity.Property(e => e.Rsn).HasColumnType("varchar(1)");

                entity.Property(e => e.Tipo)
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'U'");
            });

            modelBuilder.Entity<UsuariosLog>(entity =>
            {
                entity.ToTable("usuarios_log");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Detalle)
                    .HasColumnName("detalle")
                    .HasColumnType("text");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdUsuarios).HasColumnType("int(11)");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<PlanProyAct_Dependencia>()
                .HasOne(ppad => ppad.PPAPadre)
                .WithMany()
                .HasForeignKey(ppad => ppad.PPAPadreId);

            modelBuilder.Entity<PlanProyAct_Dependencia>()
                .HasOne(ppad => ppad.PPAHija)
                .WithMany()
                .HasForeignKey(ppad => ppad.PPAHijaId);
        }
    }
}
