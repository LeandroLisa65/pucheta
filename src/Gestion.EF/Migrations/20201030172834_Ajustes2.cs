using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class Ajustes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                table: "Planificacion_Proyecto_Actividades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                table: "Planificacion_Proyecto_Actividades");
        }
    }
}
