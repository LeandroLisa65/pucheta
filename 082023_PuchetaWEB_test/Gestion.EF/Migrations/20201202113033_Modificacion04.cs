using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class Modificacion04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidentes_ParteDiario_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes");

            migrationBuilder.DropIndex(
                name: "IX_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes");

            migrationBuilder.DropColumn(
                name: "ParteDiario_IncidentesId",
                table: "Incidentes");

            migrationBuilder.AddColumn<int>(
                name: "IncidentesId",
                table: "ParteDiario_Incidentes",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParteDiario_Incidentes_Incidentes_IncidentesId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropIndex(
                name: "IX_ParteDiario_Incidentes_IncidentesId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropColumn(
                name: "IncidentesId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.AddColumn<int>(
                name: "ParteDiario_IncidentesId",
                table: "Incidentes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes",
                column: "ParteDiario_IncidentesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidentes_ParteDiario_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes",
                column: "ParteDiario_IncidentesId",
                principalTable: "ParteDiario_Incidentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
