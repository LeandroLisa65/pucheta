using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class ParteDiario_Asitencia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParteDiario_AsistenciaId",
                table: "Contratistas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Asistencia_ParteDiarioId",
                table: "ParteDiario_Asistencia",
                column: "ParteDiarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratistas_ParteDiario_AsistenciaId",
                table: "Contratistas",
                column: "ParteDiario_AsistenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratistas_ParteDiario_Asistencia_ParteDiario_AsistenciaId",
                table: "Contratistas",
                column: "ParteDiario_AsistenciaId",
                principalTable: "ParteDiario_Asistencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParteDiario_Asistencia_ParteDiario_ParteDiarioId",
                table: "ParteDiario_Asistencia",
                column: "ParteDiarioId",
                principalTable: "ParteDiario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratistas_ParteDiario_Asistencia_ParteDiario_AsistenciaId",
                table: "Contratistas");

            migrationBuilder.DropForeignKey(
                name: "FK_ParteDiario_Asistencia_ParteDiario_ParteDiarioId",
                table: "ParteDiario_Asistencia");

            migrationBuilder.DropIndex(
                name: "IX_ParteDiario_Asistencia_ParteDiarioId",
                table: "ParteDiario_Asistencia");

            migrationBuilder.DropIndex(
                name: "IX_Contratistas_ParteDiario_AsistenciaId",
                table: "Contratistas");

            migrationBuilder.DropColumn(
                name: "ParteDiario_AsistenciaId",
                table: "Contratistas");
        }
    }
}
