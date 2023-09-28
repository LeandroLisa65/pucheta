using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class PPAFinalizados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finalizados",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalizados",
                table: "Planificacion_Proyecto_Actividades");
        }
    }
}
