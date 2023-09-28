using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParteDiario_Incidentes_Incidentes_IncidentesId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropIndex(
                name: "IX_ParteDiario_Incidentes_IncidentesId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropColumn(
                name: "ContratistasId",
                table: "Proyecto_Contratista");

            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "IncidentesId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropColumn(
                name: "Aceptacion_Descripcion",
                table: "Incidentes_Historial");

            migrationBuilder.DropColumn(
                name: "Aceptacion_Fecha",
                table: "Incidentes_Historial");

            migrationBuilder.DropColumn(
                name: "Aceptacion_UsuarioId",
                table: "Incidentes_Historial");

            migrationBuilder.DropColumn(
                name: "SolucionPropuesta_Descripcion",
                table: "Incidentes_Historial");

            migrationBuilder.DropColumn(
                name: "SolucionPropuesta_Fecha",
                table: "Incidentes_Historial");

            migrationBuilder.DropColumn(
                name: "SolucionPropuesta_UsuarioId",
                table: "Incidentes_Historial");

            migrationBuilder.AddColumn<int>(
                name: "ContratistaId",
                table: "Proyecto_Contratista",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Proyecto",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Fin",
                table: "Proyecto",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RespSH_UsuarioId",
                table: "Proyecto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Proyecto",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsAdicional",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FecEstFin_Original",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FecEstInicio_Original",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdPadre",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Monto",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "Solapable",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PartidaPresupuestariaId",
                table: "Planificacion_Actividades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "AvanceActual",
                table: "ParteDiario_Actividades_Contratista",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Finalizado",
                table: "ParteDiario_Actividades_Contratista",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NotaPedidoDetalleId",
                table: "ParteDiario_Actividades_Contratista",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NotaPedidoId",
                table: "ParteDiario_Actividades_Contratista",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParteDiarioId",
                table: "ParteDiario_Actividades_Contratista",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoRegistro",
                table: "ParteDiario_Actividades_Contratista",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrabajoAyer",
                table: "ParteDiario_Actividades_Contratista",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrabajoHoy",
                table: "ParteDiario_Actividades_Contratista",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Planificacion_ActividadesId",
                table: "ParteDiario_Actividades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Incidentes_Historial",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Cierre",
                table: "Incidentes_Historial",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ParteDiarioId",
                table: "Incidentes_Historial",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Rectype",
                table: "Incidentes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RubroId",
                table: "Contratistas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NombreOriginal",
                table: "Archivos_Adjuntos",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calidad_Actividades_Valoracion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdPlanificacion_Proyecto_ActividadId = table.Column<int>(nullable: false),
                    IdParteDiario = table.Column<int>(nullable: false),
                    IdCalidad_Items = table.Column<int>(nullable: false),
                    Valor = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: true),
                    IdIncidente = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calidad_Actividades_Valoracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calidad_Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Destino = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calidad_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificados",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProyecto = table.Column<long>(nullable: false),
                    IdContratista = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    NroCertificado = table.Column<string>(nullable: true),
                    Secuenciador = table.Column<long>(nullable: true),
                    IdUsuarioCreo = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "certificados_aplicacion_pagos",
                columns: table => new
                {
                    cap_Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cap_IdCertificados = table.Column<int>(nullable: false),
                    cap_NombreApellido = table.Column<string>(nullable: true),
                    cap_Monto = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificados_aplicacion_pagos", x => x.cap_Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificados_Contratistas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContratistaId = table.Column<int>(nullable: false),
                    ProyectoId = table.Column<int>(nullable: false),
                    FecDesde = table.Column<DateTime>(nullable: false),
                    FecHasta = table.Column<DateTime>(nullable: false),
                    CertContrAnteriorId = table.Column<int>(nullable: false),
                    FecRegistro = table.Column<DateTime>(nullable: false),
                    UsuarioRegistroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificados_Contratistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificados_Contratistas_Certificados_Contratistas_CertCont~",
                        column: x => x.CertContrAnteriorId,
                        principalTable: "Certificados_Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificados_Contratistas_Contratistas_ContratistaId",
                        column: x => x.ContratistaId,
                        principalTable: "Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificados_Contratistas_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "certificados_detalle_adicional",
                columns: table => new
                {
                    cda_Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cda_IdCertificados = table.Column<long>(nullable: true),
                    cda_UbicacionId = table.Column<long>(nullable: true),
                    cda_RubroId = table.Column<long>(nullable: true),
                    cda_ActividadaId = table.Column<long>(nullable: true),
                    cda_Comentario = table.Column<string>(nullable: true),
                    cda_UnidadMedida = table.Column<string>(nullable: true),
                    cda_Partida_Id = table.Column<long>(nullable: true),
                    cda_Importe_Cantidad_Asignada = table.Column<double>(nullable: true),
                    cda_Importe_Monto_Unitario = table.Column<double>(nullable: true),
                    cda_Importe_FHasta = table.Column<DateTime>(nullable: true),
                    cda_Moneda_Certificado_Actual = table.Column<double>(nullable: true),
                    cda_EstaAprobada = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificados_detalle_adicional", x => x.cda_Id);
                });

            migrationBuilder.CreateTable(
                name: "certificados_detalle_liquidacion",
                columns: table => new
                {
                    cdl_Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cdl_IdCertificados = table.Column<int>(nullable: false),
                    cdl_NroNotaPedido = table.Column<string>(nullable: true),
                    cdl_Acumulado_Actividades_Planificadas = table.Column<double>(nullable: true),
                    cdl_Descuento_Por_Anticipo_Porcentaje = table.Column<double>(nullable: true),
                    cdl_Descuento_Por_Anticipo_Monto = table.Column<double>(nullable: true),
                    cdl_Descuento_Por_Adelanto = table.Column<double>(nullable: true),
                    cdl_Total_Sujeto_Ajuste = table.Column<double>(nullable: true),
                    cdl_Ajuste_Ind_Base = table.Column<double>(nullable: true),
                    cdl_Ajuste_Ind_Actual = table.Column<double>(nullable: true),
                    cdl_Ajuste_Porc_Coeficiente = table.Column<double>(nullable: true),
                    cdl_Ajuste_Actualizacion = table.Column<double>(nullable: true),
                    cdl_Actividades_Adicionales = table.Column<double>(nullable: true),
                    cdl_Neto_Facturar = table.Column<double>(nullable: true),
                    cdl_Iva_Porcentaje = table.Column<double>(nullable: true),
                    cdl_Iva_Monto = table.Column<double>(nullable: true),
                    cdl_Total_A_Facturar = table.Column<double>(nullable: true),
                    cdl_Nro_Poliza = table.Column<string>(nullable: true),
                    cdl_Fondo_Reparo_Porcentaje = table.Column<double>(nullable: true),
                    cdl_Fondo_Reparo_Monto = table.Column<double>(nullable: true),
                    cdl_Total_A_Pagar = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificados_detalle_liquidacion", x => x.cdl_Id);
                });

            migrationBuilder.CreateTable(
                name: "certificados_detalle_planificado",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdCertificados = table.Column<long>(nullable: true),
                    IdPA = table.Column<long>(nullable: true),
                    RubroNombre = table.Column<string>(nullable: true),
                    RubroId = table.Column<long>(nullable: true),
                    ActividadNombre = table.Column<string>(nullable: true),
                    PartidaCodigo = table.Column<string>(nullable: true),
                    PartidaId = table.Column<long>(nullable: true),
                    IdPPA = table.Column<long>(nullable: true),
                    UbicacionId = table.Column<long>(nullable: true),
                    UbicacionNombre = table.Column<string>(nullable: true),
                    Detalle = table.Column<string>(nullable: true),
                    IdNotaPedido = table.Column<long>(nullable: true),
                    NroNotaPedido = table.Column<string>(nullable: true),
                    IdNotaPedidoDetalle = table.Column<long>(nullable: true),
                    Cantidad_Asignada = table.Column<double>(nullable: true),
                    Monto_Unitario = table.Column<double>(nullable: true),
                    Monto_Total = table.Column<double>(nullable: true),
                    Act_Presenta_Liquidacion = table.Column<bool>(nullable: true),
                    Act_Acum_Ant_Unid = table.Column<double>(nullable: true),
                    Act_F_Desde = table.Column<DateTime>(nullable: true),
                    Act_F_Hasta = table.Column<DateTime>(nullable: true),
                    Act_Avance_Periodo_Unid = table.Column<double>(nullable: true),
                    Act_Acum_Total_Unid = table.Column<double>(nullable: true),
                    Act_Acum_Ant_Moneda = table.Column<double>(nullable: true),
                    Act_Cert_Actual_Moneda = table.Column<double>(nullable: true),
                    Act_Acum_Actual_Moneda = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificados_detalle_planificado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Asunto = table.Column<string>(nullable: true),
                    Mensaje = table.Column<string>(nullable: true),
                    Remitente = table.Column<string>(nullable: true),
                    Destinatarios = table.Column<string>(nullable: true),
                    FecCreacion = table.Column<DateTime>(nullable: false),
                    Enviado = table.Column<bool>(nullable: false),
                    EntidadId = table.Column<int>(nullable: false),
                    Entidad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncidentesHistorial_Detalle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdIncidente_Historial = table.Column<int>(nullable: false),
                    Asignacion_Fecha = table.Column<DateTime>(nullable: false),
                    Asignacion_IdArea = table.Column<int>(nullable: false),
                    Asignacion_Area = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Detalle = table.Column<string>(nullable: true),
                    Modificacion_Usuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentesHistorial_Detalle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indice_Valores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdIndice = table.Column<long>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indice_Valores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(maxLength: 10, nullable: true),
                    Nombre = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Informes_Actividades_Vencidas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyectoId = table.Column<int>(nullable: false),
                    ProyectoUbicacionId = table.Column<int>(nullable: false),
                    PlanActividadId = table.Column<int>(nullable: false),
                    PlanificacionProyectoActividadId = table.Column<int>(nullable: false),
                    FecUltimoAvance = table.Column<DateTime>(nullable: false),
                    PorcAvanceObra = table.Column<float>(nullable: false),
                    Fecha_Est_Fin = table.Column<DateTime>(nullable: false),
                    DiasVencida = table.Column<int>(nullable: false),
                    FecCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informes_Actividades_Vencidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotaPedido",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProyecto = table.Column<long>(nullable: false),
                    IdContratista = table.Column<long>(nullable: false),
                    NroNP = table.Column<string>(nullable: true),
                    Comentario = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    IdUsuario_Creacion = table.Column<long>(nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(nullable: false),
                    Fecha_Cierre = table.Column<DateTime>(nullable: false),
                    Secuencial = table.Column<long>(nullable: true),
                    IdIndiceAjuste = table.Column<long>(nullable: true),
                    PresentaPoliza = table.Column<bool>(nullable: true),
                    ComentarioPoliza = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaPedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotaPedido_Detalle",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdNotaPedido = table.Column<long>(nullable: false),
                    IdPPA = table.Column<long>(nullable: false),
                    IdPA = table.Column<long>(nullable: false),
                    IdUbicacion = table.Column<long>(nullable: false),
                    IdIndiceAjuste = table.Column<long>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    Precio_Contradado = table.Column<double>(nullable: false),
                    Avance_Actual = table.Column<double>(nullable: false),
                    UnidadMedida = table.Column<string>(nullable: true),
                    Finalizado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaPedido_Detalle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(nullable: true),
                    Mensaje = table.Column<string>(nullable: true),
                    UsuarioEmisorId = table.Column<int>(nullable: false),
                    FecEmision = table.Column<DateTime>(nullable: false),
                    UsuarioReceptorId = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FecLectura = table.Column<DateTime>(nullable: false),
                    EntidadId = table.Column<int>(nullable: false),
                    EntidadNombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_usuarios_UsuarioEmisorId",
                        column: x => x.UsuarioEmisorId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificaciones_usuarios_UsuarioReceptorId",
                        column: x => x.UsuarioReceptorId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartidasPresupuestarias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidasPresupuestarias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planificacion_Actividades_Calidad_Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdPLanificacionActividades = table.Column<int>(nullable: false),
                    Documentacion_Obra = table.Column<string>(maxLength: 200, nullable: true),
                    Se_Efectua = table.Column<string>(maxLength: 50, nullable: true),
                    Perioricidad = table.Column<int>(nullable: false),
                    Tolerancia = table.Column<string>(maxLength: 50, nullable: true),
                    Realiza_Accion = table.Column<string>(maxLength: 50, nullable: true),
                    Controla = table.Column<string>(maxLength: 50, nullable: true),
                    Como_Controlar = table.Column<string>(maxLength: 50, nullable: true),
                    Con_Elemento = table.Column<string>(maxLength: 50, nullable: true),
                    Con_Plano = table.Column<string>(maxLength: 50, nullable: true),
                    Donde = table.Column<string>(maxLength: 50, nullable: true),
                    Observaciones = table.Column<string>(maxLength: 1024, nullable: true),
                    Inactivo = table.Column<bool>(nullable: false),
                    Fecha_Ult_Modificacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioMOdifico = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planificacion_Actividades_Calidad_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanProyAct_Dependencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PPAPadreId = table.Column<int>(nullable: false),
                    PPAHijaId = table.Column<int>(nullable: false),
                    FecCreacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioCreo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanProyAct_Dependencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanProyAct_Dependencias_Planificacion_Proyecto_Actividades_~",
                        column: x => x.PPAHijaId,
                        principalTable: "Planificacion_Proyecto_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanProyAct_Dependencias_Planificacion_Proyecto_Actividades~1",
                        column: x => x.PPAPadreId,
                        principalTable: "Planificacion_Proyecto_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcesosAutomaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FecCreacion = table.Column<DateTime>(nullable: false),
                    EntidadId = table.Column<int>(nullable: false),
                    Entidad = table.Column<string>(nullable: true),
                    Motivo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcesosAutomaticos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProyCertificados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyectoId = table.Column<int>(nullable: false),
                    ProyCertAnteriorId = table.Column<int>(nullable: false),
                    FecDesde = table.Column<DateTime>(nullable: false),
                    FecHasta = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FecCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyCertificados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyCertificados_ProyCertificados_ProyCertAnteriorId",
                        column: x => x.ProyCertAnteriorId,
                        principalTable: "ProyCertificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyCertificados_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyContr_Descuentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyectoId = table.Column<int>(nullable: false),
                    ContratistaId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Documento = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Monto = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyContr_Descuentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyContr_Descuentos_Contratistas_ContratistaId",
                        column: x => x.ContratistaId,
                        principalTable: "Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyContr_Descuentos_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertContrs_PDActContrs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CertContrId = table.Column<int>(nullable: false),
                    PDActContrId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<float>(nullable: false),
                    CantRealCertificada = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertContrs_PDActContrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertContrs_PDActContrs_Certificados_Contratistas_CertContrId",
                        column: x => x.CertContrId,
                        principalTable: "Certificados_Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertContrs_PDActContrs_ParteDiario_Actividades_Contratista_P~",
                        column: x => x.PDActContrId,
                        principalTable: "ParteDiario_Actividades_Contratista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CertContrs_PlanProyActs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CertContrId = table.Column<int>(nullable: false),
                    PlanProyActId = table.Column<int>(nullable: false),
                    NroItem = table.Column<int>(nullable: false),
                    Ubicacion = table.Column<string>(nullable: true),
                    Actividad = table.Column<string>(nullable: true),
                    UnidadMedida = table.Column<string>(nullable: true),
                    Partida = table.Column<string>(nullable: true),
                    CantidadPlanificada = table.Column<float>(nullable: false),
                    MontoPlanificado = table.Column<float>(nullable: false),
                    CantidadAcumAnterior = table.Column<float>(nullable: false),
                    CantidadActual = table.Column<float>(nullable: false),
                    CantidadAjuste = table.Column<float>(nullable: false),
                    ImporteAcumAnterior = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertContrs_PlanProyActs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertContrs_PlanProyActs_Certificados_Contratistas_CertContrId",
                        column: x => x.CertContrId,
                        principalTable: "Certificados_Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertContrs_PlanProyActs_Planificacion_Proyecto_Actividades_P~",
                        column: x => x.PlanProyActId,
                        principalTable: "Planificacion_Proyecto_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyCert_Contratistas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyCertificadoId = table.Column<int>(nullable: false),
                    ContratistaId = table.Column<int>(nullable: false),
                    EmiteFactura = table.Column<bool>(nullable: false),
                    PorcentajeIVA = table.Column<float>(nullable: false),
                    IndiceBase = table.Column<float>(nullable: false),
                    IndiceActual = table.Column<float>(nullable: false),
                    PorcentajeActualizacion = table.Column<float>(nullable: false),
                    Adelanto = table.Column<float>(nullable: false),
                    PorcentajeDescuentoAnticipo = table.Column<float>(nullable: false),
                    PorcentajeFondoReparo = table.Column<float>(nullable: false),
                    NumeroCertificado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyCert_Contratistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyCert_Contratistas_Contratistas_ContratistaId",
                        column: x => x.ContratistaId,
                        principalTable: "Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyCert_Contratistas_ProyCertificados_ProyCertificadoId",
                        column: x => x.ProyCertificadoId,
                        principalTable: "ProyCertificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyCert_Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyCertificadoId = table.Column<int>(nullable: false),
                    ContratistaId = table.Column<int>(nullable: false),
                    ApellidoNombre = table.Column<string>(nullable: true),
                    Monto = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyCert_Empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyCert_Empleados_Contratistas_ContratistaId",
                        column: x => x.ContratistaId,
                        principalTable: "Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyCert_Empleados_ProyCertificados_ProyCertificadoId",
                        column: x => x.ProyCertificadoId,
                        principalTable: "ProyCertificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyCert_PDActContr_Adicionales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyCertificadoId = table.Column<int>(nullable: false),
                    ContratistaId = table.Column<int>(nullable: false),
                    Actividad = table.Column<string>(nullable: true),
                    ActividadDescripcion = table.Column<string>(nullable: true),
                    Ubicacion = table.Column<string>(nullable: true),
                    UnidadMedida = table.Column<string>(nullable: true),
                    CodigoPartidaPresupuestaria = table.Column<string>(nullable: true),
                    Cantidad = table.Column<float>(nullable: false),
                    Monto = table.Column<float>(nullable: false),
                    FecCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreoId = table.Column<int>(nullable: false),
                    MontosAjustables = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyCert_PDActContr_Adicionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyCert_PDActContr_Adicionales_Contratistas_ContratistaId",
                        column: x => x.ContratistaId,
                        principalTable: "Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyCert_PDActContr_Adicionales_ProyCertificados_ProyCertifi~",
                        column: x => x.ProyCertificadoId,
                        principalTable: "ProyCertificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyCert_PDActContrs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyCertificadoId = table.Column<int>(nullable: false),
                    PDActContrId = table.Column<int>(nullable: false),
                    PlanProyActId = table.Column<int>(nullable: false),
                    ContratistaId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<float>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FecCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreoId = table.Column<int>(nullable: false),
                    MontosAjustables = table.Column<bool>(nullable: false),
                    ImporteAcumAnterior = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyCert_PDActContrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyCert_PDActContrs_Contratistas_ContratistaId",
                        column: x => x.ContratistaId,
                        principalTable: "Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyCert_PDActContrs_ParteDiario_Actividades_Contratista_PDA~",
                        column: x => x.PDActContrId,
                        principalTable: "ParteDiario_Actividades_Contratista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyCert_PDActContrs_ProyCertificados_ProyCertificadoId",
                        column: x => x.ProyCertificadoId,
                        principalTable: "ProyCertificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyCert_PlanProyActs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyCertificadoId = table.Column<int>(nullable: false),
                    PlanProyActId = table.Column<int>(nullable: false),
                    Ubicacion = table.Column<string>(nullable: true),
                    Actividad = table.Column<string>(nullable: true),
                    ActividadDescripcion = table.Column<string>(nullable: true),
                    UnidadMedida = table.Column<string>(nullable: true),
                    PartidaPresupuestariaId = table.Column<int>(nullable: false),
                    CodigoPartidaPresupuestaria = table.Column<string>(nullable: true),
                    DescripcionPartidaPresupuestaria = table.Column<string>(nullable: true),
                    CantidadPlanificada = table.Column<float>(nullable: false),
                    MontoPlanificado = table.Column<float>(nullable: false),
                    ExcedenteAutorizado = table.Column<bool>(nullable: false),
                    CantidadAutorizadaExcedente = table.Column<float>(nullable: false),
                    NotificacionId = table.Column<int>(nullable: false),
                    Visada = table.Column<bool>(nullable: false),
                    FecCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyCert_PlanProyActs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyCert_PlanProyActs_Planificacion_Proyecto_Actividades_Pla~",
                        column: x => x.PlanProyActId,
                        principalTable: "Planificacion_Proyecto_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyCert_PlanProyActs_ProyCertificados_ProyCertificadoId",
                        column: x => x.ProyCertificadoId,
                        principalTable: "ProyCertificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_Contratista_ContratistaId",
                table: "Proyecto_Contratista",
                column: "ContratistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_Contratista_ProyectoId",
                table: "Proyecto_Contratista",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_RespSH_UsuarioId",
                table: "Proyecto",
                column: "RespSH_UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Planificacion_Actividades_PartidaPresupuestariaId",
                table: "Planificacion_Actividades",
                column: "PartidaPresupuestariaId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Incidentes_ContratistaId",
                table: "ParteDiario_Incidentes",
                column: "ContratistaId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Incidentes_IncidenteId",
                table: "ParteDiario_Incidentes",
                column: "IncidenteId");

            migrationBuilder.CreateIndex(
                name: "IX_CertContrs_PDActContrs_CertContrId",
                table: "CertContrs_PDActContrs",
                column: "CertContrId");

            migrationBuilder.CreateIndex(
                name: "IX_CertContrs_PDActContrs_PDActContrId",
                table: "CertContrs_PDActContrs",
                column: "PDActContrId");

            migrationBuilder.CreateIndex(
                name: "IX_CertContrs_PlanProyActs_CertContrId",
                table: "CertContrs_PlanProyActs",
                column: "CertContrId");

            migrationBuilder.CreateIndex(
                name: "IX_CertContrs_PlanProyActs_PlanProyActId",
                table: "CertContrs_PlanProyActs",
                column: "PlanProyActId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Contratistas_CertContrAnteriorId",
                table: "Certificados_Contratistas",
                column: "CertContrAnteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Contratistas_ContratistaId",
                table: "Certificados_Contratistas",
                column: "ContratistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Contratistas_ProyectoId",
                table: "Certificados_Contratistas",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioEmisorId",
                table: "Notificaciones",
                column: "UsuarioEmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioReceptorId",
                table: "Notificaciones",
                column: "UsuarioReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanProyAct_Dependencias_PPAHijaId",
                table: "PlanProyAct_Dependencias",
                column: "PPAHijaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanProyAct_Dependencias_PPAPadreId",
                table: "PlanProyAct_Dependencias",
                column: "PPAPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_Contratistas_ContratistaId",
                table: "ProyCert_Contratistas",
                column: "ContratistaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_Contratistas_ProyCertificadoId",
                table: "ProyCert_Contratistas",
                column: "ProyCertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_Empleados_ContratistaId",
                table: "ProyCert_Empleados",
                column: "ContratistaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_Empleados_ProyCertificadoId",
                table: "ProyCert_Empleados",
                column: "ProyCertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_PDActContr_Adicionales_ContratistaId",
                table: "ProyCert_PDActContr_Adicionales",
                column: "ContratistaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_PDActContr_Adicionales_ProyCertificadoId",
                table: "ProyCert_PDActContr_Adicionales",
                column: "ProyCertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_PDActContrs_ContratistaId",
                table: "ProyCert_PDActContrs",
                column: "ContratistaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_PDActContrs_PDActContrId",
                table: "ProyCert_PDActContrs",
                column: "PDActContrId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_PDActContrs_ProyCertificadoId",
                table: "ProyCert_PDActContrs",
                column: "ProyCertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_PlanProyActs_PlanProyActId",
                table: "ProyCert_PlanProyActs",
                column: "PlanProyActId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCert_PlanProyActs_ProyCertificadoId",
                table: "ProyCert_PlanProyActs",
                column: "ProyCertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCertificados_ProyCertAnteriorId",
                table: "ProyCertificados",
                column: "ProyCertAnteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyCertificados_ProyectoId",
                table: "ProyCertificados",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyContr_Descuentos_ContratistaId",
                table: "ProyContr_Descuentos",
                column: "ContratistaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyContr_Descuentos_ProyectoId",
                table: "ProyContr_Descuentos",
                column: "ProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParteDiario_Incidentes_Contratistas_ContratistaId",
                table: "ParteDiario_Incidentes",
                column: "ContratistaId",
                principalTable: "Contratistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParteDiario_Incidentes_Incidentes_IncidenteId",
                table: "ParteDiario_Incidentes",
                column: "IncidenteId",
                principalTable: "Incidentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Planificacion_Actividades_PartidasPresupuestarias_PartidaPre~",
                table: "Planificacion_Actividades",
                column: "PartidaPresupuestariaId",
                principalTable: "PartidasPresupuestarias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proyecto_usuarios_RespSH_UsuarioId",
                table: "Proyecto",
                column: "RespSH_UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proyecto_Contratista_Contratistas_ContratistaId",
                table: "Proyecto_Contratista",
                column: "ContratistaId",
                principalTable: "Contratistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proyecto_Contratista_Proyecto_ProyectoId",
                table: "Proyecto_Contratista",
                column: "ProyectoId",
                principalTable: "Proyecto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParteDiario_Incidentes_Contratistas_ContratistaId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropForeignKey(
                name: "FK_ParteDiario_Incidentes_Incidentes_IncidenteId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Planificacion_Actividades_PartidasPresupuestarias_PartidaPre~",
                table: "Planificacion_Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyecto_usuarios_RespSH_UsuarioId",
                table: "Proyecto");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyecto_Contratista_Contratistas_ContratistaId",
                table: "Proyecto_Contratista");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyecto_Contratista_Proyecto_ProyectoId",
                table: "Proyecto_Contratista");

            migrationBuilder.DropTable(
                name: "Calidad_Actividades_Valoracion");

            migrationBuilder.DropTable(
                name: "Calidad_Items");

            migrationBuilder.DropTable(
                name: "CertContrs_PDActContrs");

            migrationBuilder.DropTable(
                name: "CertContrs_PlanProyActs");

            migrationBuilder.DropTable(
                name: "Certificados");

            migrationBuilder.DropTable(
                name: "certificados_aplicacion_pagos");

            migrationBuilder.DropTable(
                name: "certificados_detalle_adicional");

            migrationBuilder.DropTable(
                name: "certificados_detalle_liquidacion");

            migrationBuilder.DropTable(
                name: "certificados_detalle_planificado");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "IncidentesHistorial_Detalle");

            migrationBuilder.DropTable(
                name: "Indice_Valores");

            migrationBuilder.DropTable(
                name: "Indices");

            migrationBuilder.DropTable(
                name: "Informes_Actividades_Vencidas");

            migrationBuilder.DropTable(
                name: "NotaPedido");

            migrationBuilder.DropTable(
                name: "NotaPedido_Detalle");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "PartidasPresupuestarias");

            migrationBuilder.DropTable(
                name: "Planificacion_Actividades_Calidad_Items");

            migrationBuilder.DropTable(
                name: "PlanProyAct_Dependencias");

            migrationBuilder.DropTable(
                name: "ProcesosAutomaticos");

            migrationBuilder.DropTable(
                name: "ProyCert_Contratistas");

            migrationBuilder.DropTable(
                name: "ProyCert_Empleados");

            migrationBuilder.DropTable(
                name: "ProyCert_PDActContr_Adicionales");

            migrationBuilder.DropTable(
                name: "ProyCert_PDActContrs");

            migrationBuilder.DropTable(
                name: "ProyCert_PlanProyActs");

            migrationBuilder.DropTable(
                name: "ProyContr_Descuentos");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Certificados_Contratistas");

            migrationBuilder.DropTable(
                name: "ProyCertificados");

            migrationBuilder.DropIndex(
                name: "IX_Proyecto_Contratista_ContratistaId",
                table: "Proyecto_Contratista");

            migrationBuilder.DropIndex(
                name: "IX_Proyecto_Contratista_ProyectoId",
                table: "Proyecto_Contratista");

            migrationBuilder.DropIndex(
                name: "IX_Proyecto_RespSH_UsuarioId",
                table: "Proyecto");

            migrationBuilder.DropIndex(
                name: "IX_Planificacion_Actividades_PartidaPresupuestariaId",
                table: "Planificacion_Actividades");

            migrationBuilder.DropIndex(
                name: "IX_ParteDiario_Incidentes_ContratistaId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropIndex(
                name: "IX_ParteDiario_Incidentes_IncidenteId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropColumn(
                name: "ContratistaId",
                table: "Proyecto_Contratista");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "Fecha_Fin",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "RespSH_UsuarioId",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "EsAdicional",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "FecEstFin_Original",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "FecEstInicio_Original",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "IdPadre",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "Solapable",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "PartidaPresupuestariaId",
                table: "Planificacion_Actividades");

            migrationBuilder.DropColumn(
                name: "AvanceActual",
                table: "ParteDiario_Actividades_Contratista");

            migrationBuilder.DropColumn(
                name: "Finalizado",
                table: "ParteDiario_Actividades_Contratista");

            migrationBuilder.DropColumn(
                name: "NotaPedidoDetalleId",
                table: "ParteDiario_Actividades_Contratista");

            migrationBuilder.DropColumn(
                name: "NotaPedidoId",
                table: "ParteDiario_Actividades_Contratista");

            migrationBuilder.DropColumn(
                name: "ParteDiarioId",
                table: "ParteDiario_Actividades_Contratista");

            migrationBuilder.DropColumn(
                name: "TipoRegistro",
                table: "ParteDiario_Actividades_Contratista");

            migrationBuilder.DropColumn(
                name: "TrabajoAyer",
                table: "ParteDiario_Actividades_Contratista");

            migrationBuilder.DropColumn(
                name: "TrabajoHoy",
                table: "ParteDiario_Actividades_Contratista");

            migrationBuilder.DropColumn(
                name: "Planificacion_ActividadesId",
                table: "ParteDiario_Actividades");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Incidentes_Historial");

            migrationBuilder.DropColumn(
                name: "Fecha_Cierre",
                table: "Incidentes_Historial");

            migrationBuilder.DropColumn(
                name: "ParteDiarioId",
                table: "Incidentes_Historial");

            migrationBuilder.DropColumn(
                name: "Rectype",
                table: "Incidentes");

            migrationBuilder.DropColumn(
                name: "RubroId",
                table: "Contratistas");

            migrationBuilder.DropColumn(
                name: "NombreOriginal",
                table: "Archivos_Adjuntos");

            migrationBuilder.AddColumn<int>(
                name: "ContratistasId",
                table: "Proyecto_Contratista",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duracion",
                table: "Proyecto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IncidentesId",
                table: "ParteDiario_Incidentes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Aceptacion_Descripcion",
                table: "Incidentes_Historial",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Aceptacion_Fecha",
                table: "Incidentes_Historial",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Aceptacion_UsuarioId",
                table: "Incidentes_Historial",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SolucionPropuesta_Descripcion",
                table: "Incidentes_Historial",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SolucionPropuesta_Fecha",
                table: "Incidentes_Historial",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SolucionPropuesta_UsuarioId",
                table: "Incidentes_Historial",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Incidentes_IncidentesId",
                table: "ParteDiario_Incidentes",
                column: "IncidentesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParteDiario_Incidentes_Incidentes_IncidentesId",
                table: "ParteDiario_Incidentes",
                column: "IncidentesId",
                principalTable: "Incidentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
