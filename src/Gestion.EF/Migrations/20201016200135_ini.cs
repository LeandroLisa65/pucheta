using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class ini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Detalle = table.Column<string>(nullable: false),
                    MenuId = table.Column<int>(nullable: false),
                    MenuDetalle = table.Column<string>(nullable: true),
                    Editar = table.Column<bool>(nullable: false),
                    Borrar = table.Column<bool>(nullable: false),
                    OcultarZona1 = table.Column<bool>(nullable: false),
                    OcultarZona2 = table.Column<bool>(nullable: false),
                    OcultarZona3 = table.Column<bool>(nullable: false),
                    OcultarZona4 = table.Column<bool>(nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    Borrado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dispositivos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Detalle = table.Column<string>(maxLength: 50, nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispositivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdMenuPadre = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'0'"),
                    Nombre = table.Column<string>(type: "varchar(45)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", nullable: true),
                    Path = table.Column<string>(type: "varchar(255)", nullable: false),
                    Icon = table.Column<string>(type: "varchar(45)", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    Orden = table.Column<int>(type: "int(11)", nullable: true),
                    Activo = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    Borrado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planificacion_Actividades_Rubro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 250, nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planificacion_Actividades_Rubro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Provincia = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Detalle = table.Column<string>(nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    Borrado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nUsuario = table.Column<string>(type: "varchar(50)", nullable: true),
                    Nombre = table.Column<string>(type: "varchar(45)", nullable: false),
                    Apellido = table.Column<string>(type: "varchar(45)", nullable: false),
                    Clave = table.Column<string>(type: "varchar(250)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(1)", nullable: true, defaultValueSql: "'U'"),
                    Grupo = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'1'"),
                    Avatar = table.Column<string>(type: "varchar(250)", nullable: true),
                    Rsid = table.Column<string>(type: "varchar(250)", nullable: true),
                    Rsn = table.Column<string>(type: "varchar(1)", nullable: true),
                    Activo = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    Borrado = table.Column<bool>(nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUsuarios = table.Column<int>(type: "int(11)", nullable: true),
                    IP = table.Column<string>(type: "varchar(45)", maxLength: 20, nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    detalle = table.Column<string>(type: "text", maxLength: 250, nullable: true),
                    AccionesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planificacion_Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Planificacion_Actividades_RubroId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 250, nullable: false),
                    Descripción = table.Column<string>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planificacion_Actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planificacion_Actividades_Planificacion_Actividades_Rubro_Pl~",
                        column: x => x.Planificacion_Actividades_RubroId,
                        principalTable: "Planificacion_Actividades_Rubro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProvinciasId = table.Column<int>(nullable: false),
                    Localidad = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localidades_Provincias_ProvinciasId",
                        column: x => x.ProvinciasId,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccionesRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RolesId = table.Column<int>(nullable: false),
                    AccionesID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccionesRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccionesRoles_Acciones_AccionesID",
                        column: x => x.AccionesID,
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccionesRoles_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 250, nullable: false),
                    Fecha_Inicio = table.Column<DateTime>(nullable: false),
                    Duracion = table.Column<int>(nullable: false),
                    UsuariosId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyecto_usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuariosId = table.Column<int>(nullable: false),
                    RolesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosDireccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuariosId = table.Column<int>(nullable: false),
                    ProvinciasId = table.Column<int>(nullable: false),
                    LocalidadesId = table.Column<int>(nullable: false),
                    Barrio = table.Column<string>(maxLength: 250, nullable: true),
                    Calle = table.Column<string>(maxLength: 250, nullable: true),
                    Altura = table.Column<string>(maxLength: 50, nullable: true),
                    Piso = table.Column<string>(maxLength: 50, nullable: true),
                    Dpto = table.Column<string>(maxLength: 50, nullable: true),
                    CP = table.Column<string>(maxLength: 50, nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    Borrado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosDireccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosDireccion_usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosDispositivo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuariosId = table.Column<int>(nullable: false),
                    DispositivosId = table.Column<int>(nullable: false),
                    Numero = table.Column<string>(maxLength: 50, nullable: true),
                    IMEI = table.Column<string>(maxLength: 250, nullable: true),
                    FINGERTMP = table.Column<string>(nullable: true),
                    ImageBase64 = table.Column<string>(nullable: true),
                    OS = table.Column<string>(maxLength: 50, nullable: true),
                    Empresa = table.Column<string>(maxLength: 50, nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    Borrado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosDispositivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosDispositivo_usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planificacion_Actividades_Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Planificacion_ActividadesId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 250, nullable: false),
                    Descripción = table.Column<string>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planificacion_Actividades_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planificacion_Actividades_Tareas_Planificacion_Actividades_P~",
                        column: x => x.Planificacion_ActividadesId,
                        principalTable: "Planificacion_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParteDiario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyectoId = table.Column<int>(nullable: false),
                    Asig_Propios = table.Column<int>(nullable: false),
                    Asig_Propios_Presentes = table.Column<int>(nullable: false),
                    Asig_Contratista = table.Column<int>(nullable: false),
                    Asig_Contratista_Presentes = table.Column<int>(nullable: false),
                    Asig_Comentario = table.Column<string>(maxLength: 250, nullable: true),
                    Covid_Propioa = table.Column<int>(nullable: false),
                    Covid_Contratista = table.Column<int>(nullable: false),
                    Covid_Comentario = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParteDiario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto_Ubicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 250, nullable: false),
                    Descripción = table.Column<string>(nullable: false),
                    ProyectoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto_Ubicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyecto_Ubicaciones_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParteDiario_Contratista",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParteDiarioId = table.Column<int>(nullable: false),
                    UsuariosId = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParteDiario_Contratista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Contratista_ParteDiario_ParteDiarioId",
                        column: x => x.ParteDiarioId,
                        principalTable: "ParteDiario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Contratista_usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParteDiario_Sol_ObrasAdic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParteDiarioId = table.Column<int>(nullable: false),
                    Obra_Adicional = table.Column<string>(maxLength: 250, nullable: true),
                    Estado = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParteDiario_Sol_ObrasAdic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Sol_ObrasAdic_ParteDiario_ParteDiarioId",
                        column: x => x.ParteDiarioId,
                        principalTable: "ParteDiario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planificacion_Proyecto_Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Proyecto_UbicacionesId = table.Column<int>(nullable: false),
                    Planificacion_ActividadesId = table.Column<int>(nullable: false),
                    Planificacion_Actividades_RubroId = table.Column<int>(nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(nullable: false),
                    Fecha_Ultima_Modificacion = table.Column<DateTime>(nullable: false),
                    Fecha_Real_Incio = table.Column<DateTime>(nullable: false),
                    Fecha_Real_Fin = table.Column<DateTime>(nullable: false),
                    UsuariosId = table.Column<int>(nullable: false),
                    Detalle = table.Column<string>(nullable: true),
                    ReTrabajo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planificacion_Proyecto_Actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planificacion_Proyecto_Actividades_Planificacion_Actividades~",
                        column: x => x.Planificacion_ActividadesId,
                        principalTable: "Planificacion_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planificacion_Proyecto_Actividades_Planificacion_Actividade~1",
                        column: x => x.Planificacion_Actividades_RubroId,
                        principalTable: "Planificacion_Actividades_Rubro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planificacion_Proyecto_Actividades_Proyecto_Ubicaciones_Proy~",
                        column: x => x.Proyecto_UbicacionesId,
                        principalTable: "Proyecto_Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planificacion_Proyecto_Actividades_usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contratistas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 250, nullable: false),
                    Barrio = table.Column<string>(maxLength: 250, nullable: false),
                    Calle = table.Column<string>(maxLength: 250, nullable: false),
                    Altura = table.Column<string>(maxLength: 50, nullable: false),
                    Piso = table.Column<string>(maxLength: 50, nullable: true),
                    Dpto = table.Column<string>(maxLength: 50, nullable: true),
                    CP = table.Column<string>(maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(maxLength: 50, nullable: true),
                    Celular = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    ParteDiarioId = table.Column<int>(nullable: true),
                    Planificacion_Proyecto_ActividadesId = table.Column<int>(nullable: true),
                    UsuariosId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratistas_ParteDiario_ParteDiarioId",
                        column: x => x.ParteDiarioId,
                        principalTable: "ParteDiario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contratistas_Planificacion_Proyecto_Actividades_Planificacio~",
                        column: x => x.Planificacion_Proyecto_ActividadesId,
                        principalTable: "Planificacion_Proyecto_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contratistas_usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParteDiario_Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParteDiarioId = table.Column<int>(nullable: false),
                    Planificacion_Proyecto_ActividadesId = table.Column<int>(nullable: false),
                    UsuariosId = table.Column<int>(nullable: false),
                    Avance = table.Column<int>(nullable: false),
                    Finalizados = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParteDiario_Actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Actividades_ParteDiario_ParteDiarioId",
                        column: x => x.ParteDiarioId,
                        principalTable: "ParteDiario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Actividades_Planificacion_Proyecto_Actividades_P~",
                        column: x => x.Planificacion_Proyecto_ActividadesId,
                        principalTable: "Planificacion_Proyecto_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Actividades_usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planificacion_Proyecto_Actividades_Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Planificacion_Proyecto_ActividadesId = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Fecha_Est_Incio = table.Column<DateTime>(nullable: false),
                    Fecha_Est_Fin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planificacion_Proyecto_Actividades_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planificacion_Proyecto_Actividades_Log_Planificacion_Proyect~",
                        column: x => x.Planificacion_Proyecto_ActividadesId,
                        principalTable: "Planificacion_Proyecto_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParteDiario_Actividades_Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParteDiario_ActividadesId = table.Column<int>(nullable: false),
                    URL_Foto = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParteDiario_Actividades_Fotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Actividades_Fotos_ParteDiario_Actividades_ParteD~",
                        column: x => x.ParteDiario_ActividadesId,
                        principalTable: "ParteDiario_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccionesRoles_AccionesID",
                table: "AccionesRoles",
                column: "AccionesID");

            migrationBuilder.CreateIndex(
                name: "IX_AccionesRoles_RolesId",
                table: "AccionesRoles",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratistas_ParteDiarioId",
                table: "Contratistas",
                column: "ParteDiarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratistas_Planificacion_Proyecto_ActividadesId",
                table: "Contratistas",
                column: "Planificacion_Proyecto_ActividadesId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratistas_UsuariosId",
                table: "Contratistas",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_Localidades_ProvinciasId",
                table: "Localidades",
                column: "ProvinciasId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_ProyectoId",
                table: "ParteDiario",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Actividades_ParteDiarioId",
                table: "ParteDiario_Actividades",
                column: "ParteDiarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Actividades_Planificacion_Proyecto_ActividadesId",
                table: "ParteDiario_Actividades",
                column: "Planificacion_Proyecto_ActividadesId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Actividades_UsuariosId",
                table: "ParteDiario_Actividades",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Actividades_Fotos_ParteDiario_ActividadesId",
                table: "ParteDiario_Actividades_Fotos",
                column: "ParteDiario_ActividadesId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Contratista_ParteDiarioId",
                table: "ParteDiario_Contratista",
                column: "ParteDiarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Contratista_UsuariosId",
                table: "ParteDiario_Contratista",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Sol_ObrasAdic_ParteDiarioId",
                table: "ParteDiario_Sol_ObrasAdic",
                column: "ParteDiarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Planificacion_Actividades_Planificacion_Actividades_RubroId",
                table: "Planificacion_Actividades",
                column: "Planificacion_Actividades_RubroId");

            migrationBuilder.CreateIndex(
                name: "IX_Planificacion_Actividades_Tareas_Planificacion_ActividadesId",
                table: "Planificacion_Actividades_Tareas",
                column: "Planificacion_ActividadesId");

            migrationBuilder.CreateIndex(
                name: "IX_Planificacion_Proyecto_Actividades_Planificacion_Actividades~",
                table: "Planificacion_Proyecto_Actividades",
                column: "Planificacion_ActividadesId");

            migrationBuilder.CreateIndex(
                name: "IX_Planificacion_Proyecto_Actividades_Planificacion_Actividade~1",
                table: "Planificacion_Proyecto_Actividades",
                column: "Planificacion_Actividades_RubroId");

            migrationBuilder.CreateIndex(
                name: "IX_Planificacion_Proyecto_Actividades_Proyecto_UbicacionesId",
                table: "Planificacion_Proyecto_Actividades",
                column: "Proyecto_UbicacionesId");

            migrationBuilder.CreateIndex(
                name: "IX_Planificacion_Proyecto_Actividades_UsuariosId",
                table: "Planificacion_Proyecto_Actividades",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_Planificacion_Proyecto_Actividades_Log_Planificacion_Proyect~",
                table: "Planificacion_Proyecto_Actividades_Log",
                column: "Planificacion_Proyecto_ActividadesId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_UsuariosId",
                table: "Proyecto",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_Ubicaciones_ProyectoId",
                table: "Proyecto_Ubicaciones",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsuarios_RolesId",
                table: "RolesUsuarios",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsuarios_UsuariosId",
                table: "RolesUsuarios",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosDireccion_UsuariosId",
                table: "UsuariosDireccion",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosDispositivo_UsuariosId",
                table: "UsuariosDispositivo",
                column: "UsuariosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccionesRoles");

            migrationBuilder.DropTable(
                name: "Contratistas");

            migrationBuilder.DropTable(
                name: "Dispositivos");

            migrationBuilder.DropTable(
                name: "Localidades");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "ParteDiario_Actividades_Fotos");

            migrationBuilder.DropTable(
                name: "ParteDiario_Contratista");

            migrationBuilder.DropTable(
                name: "ParteDiario_Sol_ObrasAdic");

            migrationBuilder.DropTable(
                name: "Planificacion_Actividades_Tareas");

            migrationBuilder.DropTable(
                name: "Planificacion_Proyecto_Actividades_Log");

            migrationBuilder.DropTable(
                name: "RolesUsuarios");

            migrationBuilder.DropTable(
                name: "usuarios_log");

            migrationBuilder.DropTable(
                name: "UsuariosDireccion");

            migrationBuilder.DropTable(
                name: "UsuariosDispositivo");

            migrationBuilder.DropTable(
                name: "Acciones");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.DropTable(
                name: "ParteDiario_Actividades");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ParteDiario");

            migrationBuilder.DropTable(
                name: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropTable(
                name: "Planificacion_Actividades");

            migrationBuilder.DropTable(
                name: "Proyecto_Ubicaciones");

            migrationBuilder.DropTable(
                name: "Planificacion_Actividades_Rubro");

            migrationBuilder.DropTable(
                name: "Proyecto");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
