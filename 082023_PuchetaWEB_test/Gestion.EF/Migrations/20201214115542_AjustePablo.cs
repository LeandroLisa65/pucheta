using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class AjustePablo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContratistaId",
                table: "ParteDiario_Incidentes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "GeneraSegIncidente",
                table: "Incidentes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContratistaId",
                table: "ParteDiario_Incidentes");

            migrationBuilder.DropColumn(
                name: "GeneraSegIncidente",
                table: "Incidentes");
        }
    }
}
