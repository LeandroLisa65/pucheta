using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class Incidentes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParteDiario_Incidentes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParteDiarioId = table.Column<int>(nullable: false),
                    IncidenteId = table.Column<int>(nullable: false),
                    NoConformidadId = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    SolicitadoPor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParteDiario_Incidentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Incidentes_ParteDiario_ParteDiarioId",
                        column: x => x.ParteDiarioId,
                        principalTable: "ParteDiario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidentes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    TipoIncidenteId = table.Column<int>(nullable: false),
                    Criticidad = table.Column<string>(maxLength: 1, nullable: true),
                    GeneraEmail = table.Column<bool>(nullable: false),
                    ListaEmail = table.Column<string>(nullable: true),
                    RolResponsable = table.Column<string>(nullable: true),
                    ParteDiario_IncidentesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidentes_ParteDiario_Incidentes_ParteDiario_IncidentesId",
                        column: x => x.ParteDiario_IncidentesId,
                        principalTable: "ParteDiario_Incidentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncidentesTipo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    IncidentesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentesTipo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncidentesTipo_Incidentes_IncidentesId",
                        column: x => x.IncidentesId,
                        principalTable: "Incidentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes",
                column: "ParteDiario_IncidentesId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentesTipo_IncidentesId",
                table: "IncidentesTipo",
                column: "IncidentesId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Incidentes_ParteDiarioId",
                table: "ParteDiario_Incidentes",
                column: "ParteDiarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncidentesTipo");

            migrationBuilder.DropTable(
                name: "Incidentes");

            migrationBuilder.DropTable(
                name: "ParteDiario_Incidentes");
        }
    }
}
