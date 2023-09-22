using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class ParteDiarioCont : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Avance",
                table: "Planificacion_Proyecto_Actividades",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Finalizados",
                table: "ParteDiario_Actividades",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Avance",
                table: "ParteDiario_Actividades",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Comentarios",
                table: "ParteDiario_Actividades",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParteDiario_Actividades_Contratista",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParteDiario_ActividadesId = table.Column<int>(nullable: false),
                    ContratistasId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<float>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParteDiario_Actividades_Contratista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Actividades_Contratista_Contratistas_Contratista~",
                        column: x => x.ContratistasId,
                        principalTable: "Contratistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParteDiario_Actividades_Contratista_ParteDiario_Actividades_~",
                        column: x => x.ParteDiario_ActividadesId,
                        principalTable: "ParteDiario_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Actividades_Contratista_ContratistasId",
                table: "ParteDiario_Actividades_Contratista",
                column: "ContratistasId");

            migrationBuilder.CreateIndex(
                name: "IX_ParteDiario_Actividades_Contratista_ParteDiario_ActividadesId",
                table: "ParteDiario_Actividades_Contratista",
                column: "ParteDiario_ActividadesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParteDiario_Actividades_Contratista");

            migrationBuilder.DropColumn(
                name: "Comentarios",
                table: "ParteDiario_Actividades");

            migrationBuilder.AlterColumn<int>(
                name: "Avance",
                table: "Planificacion_Proyecto_Actividades",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Finalizados",
                table: "ParteDiario_Actividades",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "Avance",
                table: "ParteDiario_Actividades",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
