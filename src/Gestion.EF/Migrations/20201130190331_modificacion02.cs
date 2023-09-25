using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class modificacion02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratistas_ParteDiario_Asistencia_ParteDiario_AsistenciaId",
                table: "Contratistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidentes_ParteDiario_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncidentesTipo_Incidentes_IncidentesId",
                table: "IncidentesTipo");

            migrationBuilder.DropIndex(
                name: "IX_IncidentesTipo_IncidentesId",
                table: "IncidentesTipo");

            migrationBuilder.DropIndex(
                name: "IX_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes");

            migrationBuilder.DropIndex(
                name: "IX_Contratistas_ParteDiario_AsistenciaId",
                table: "Contratistas");

            migrationBuilder.DropColumn(
                name: "IncidentesId",
                table: "IncidentesTipo");

            migrationBuilder.DropColumn(
                name: "ParteDiario_AsistenciaId",
                table: "Contratistas");

            migrationBuilder.AlterColumn<int>(
                name: "ParteDiario_IncidentesId",
                table: "Incidentes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncidentesTipoId",
                table: "Incidentes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Asistencia_ContratistasId",
                table: "ParteDiario_Asistencia",
                column: "ContratistasId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_IncidentesTipoId",
                table: "Incidentes",
                column: "IncidentesTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes",
                column: "ParteDiario_IncidentesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidentes_IncidentesTipo_IncidentesTipoId",
                table: "Incidentes",
                column: "IncidentesTipoId",
                principalTable: "IncidentesTipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidentes_ParteDiario_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes",
                column: "ParteDiario_IncidentesId",
                principalTable: "ParteDiario_Incidentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParteDiario_Asistencia_Contratistas_ContratistasId",
                table: "ParteDiario_Asistencia",
                column: "ContratistasId",
                principalTable: "Contratistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidentes_IncidentesTipo_IncidentesTipoId",
                table: "Incidentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidentes_ParteDiario_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes");

            migrationBuilder.DropForeignKey(
                name: "FK_ParteDiario_Asistencia_Contratistas_ContratistasId",
                table: "ParteDiario_Asistencia");

            migrationBuilder.DropIndex(
                name: "IX_ParteDiario_Asistencia_ContratistasId",
                table: "ParteDiario_Asistencia");

            migrationBuilder.DropIndex(
                name: "IX_Incidentes_IncidentesTipoId",
                table: "Incidentes");

            migrationBuilder.DropIndex(
                name: "IX_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes");

            migrationBuilder.DropColumn(
                name: "IncidentesTipoId",
                table: "Incidentes");

            migrationBuilder.AddColumn<int>(
                name: "IncidentesId",
                table: "IncidentesTipo",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParteDiario_IncidentesId",
                table: "Incidentes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ParteDiario_AsistenciaId",
                table: "Contratistas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncidentesTipo_IncidentesId",
                table: "IncidentesTipo",
                column: "IncidentesId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes",
                column: "ParteDiario_IncidentesId");

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
                name: "FK_Incidentes_ParteDiario_Incidentes_ParteDiario_IncidentesId",
                table: "Incidentes",
                column: "ParteDiario_IncidentesId",
                principalTable: "ParteDiario_Incidentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentesTipo_Incidentes_IncidentesId",
                table: "IncidentesTipo",
                column: "IncidentesId",
                principalTable: "Incidentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
