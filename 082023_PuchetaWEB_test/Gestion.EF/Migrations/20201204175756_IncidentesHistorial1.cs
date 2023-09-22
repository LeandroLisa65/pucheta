using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class IncidentesHistorial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incidentes_Historial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyectoId = table.Column<int>(nullable: false),
                    ContratistasId = table.Column<int>(nullable: false),
                    IncidenteId = table.Column<int>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    Creacion_Descripcion = table.Column<string>(nullable: true),
                    Creacion_Fecha = table.Column<DateTime>(nullable: false),
                    Creacion_UsuarioId = table.Column<int>(nullable: false),
                    SolucionPropuesta_Descripcion = table.Column<string>(nullable: true),
                    SolucionPropuesta_Fecha = table.Column<DateTime>(nullable: false),
                    SolucionPropuesta_UsuarioId = table.Column<int>(nullable: false),
                    Aceptacion_Descripcion = table.Column<string>(nullable: true),
                    Aceptacion_Fecha = table.Column<DateTime>(nullable: false),
                    Aceptacion_UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidentes_Historial", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidentes_Historial");
        }
    }
}
