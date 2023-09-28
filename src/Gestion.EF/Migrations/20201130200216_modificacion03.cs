using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class modificacion03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidentes_IncidentesTipo_IncidentesTipoId",
                table: "Incidentes");

            migrationBuilder.DropIndex(
                name: "IX_Incidentes_IncidentesTipoId",
                table: "Incidentes");

            migrationBuilder.DropColumn(
                name: "IncidentesTipoId",
                table: "Incidentes");

            migrationBuilder.AlterColumn<string>(
                name: "Criticidad",
                table: "Incidentes",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1) CHARACTER SET utf8mb4",
                oldMaxLength: 1,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Criticidad",
                table: "Incidentes",
                type: "varchar(1) CHARACTER SET utf8mb4",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncidentesTipoId",
                table: "Incidentes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_IncidentesTipoId",
                table: "Incidentes",
                column: "IncidentesTipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidentes_IncidentesTipo_IncidentesTipoId",
                table: "Incidentes",
                column: "IncidentesTipoId",
                principalTable: "IncidentesTipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
