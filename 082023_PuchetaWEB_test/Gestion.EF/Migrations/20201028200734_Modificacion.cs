using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class Modificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Avance",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Est_Fin",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Est_Incio",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avance",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "Fecha_Est_Fin",
                table: "Planificacion_Proyecto_Actividades");

            migrationBuilder.DropColumn(
                name: "Fecha_Est_Incio",
                table: "Planificacion_Proyecto_Actividades");
        }
    }
}
