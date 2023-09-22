using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class contratista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratistas_Planificacion_Proyecto_Actividades_Planificacio~",
                table: "Contratistas");

            migrationBuilder.DropIndex(
                name: "IX_Contratistas_Planificacion_Proyecto_ActividadesId",
                table: "Contratistas");

            migrationBuilder.DropColumn(
                name: "Planificacion_Proyecto_ActividadesId",
                table: "Contratistas");

            migrationBuilder.AddColumn<int>(
                name: "ContratistasId",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Planificacion_Proyecto_Actividades_ContratistasId",
                table: "Planificacion_Proyecto_Actividades",
                column: "ContratistasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planificacion_Proyecto_Actividades_Contratistas_Contratistas~",
                table: "Planificacion_Proyecto_Actividades",
                column: "ContratistasId",
                principalTable: "Contratistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planificacion_Proyecto_Actividades_Contratistas_Contratistas~",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropIndex(
                name: "IX_Planificacion_Proyecto_Actividades_ContratistasId",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "ContratistasId",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.AddColumn<int>(
                name: "Planificacion_Proyecto_ActividadesId",
                table: "Contratistas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contratistas_Planificacion_Proyecto_ActividadesId",
                table: "Contratistas",
                column: "Planificacion_Proyecto_ActividadesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratistas_Planificacion_Proyecto_Actividades_Planificacio~",
                table: "Contratistas",
                column: "Planificacion_Proyecto_ActividadesId",
                principalTable: "Planificacion_Proyecto_Actividades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
