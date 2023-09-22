using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class Adjuntos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archivos_Adjuntos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Archivo = table.Column<string>(maxLength: 250, nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    URL = table.Column<string>(maxLength: 250, nullable: true),
                    Extencion = table.Column<string>(maxLength: 25, nullable: true),
                    IdUsuario = table.Column<int>(nullable: false),
                    sUsuario = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivos_Adjuntos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Archivos_Adjuntos_Relacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Archivos_AdjuntosId = table.Column<int>(nullable: false),
                    Entidad = table.Column<string>(maxLength: 5, nullable: true),
                    IdEntidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivos_Adjuntos_Relacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Archivos_Adjuntos_Relacion_Archivos_Adjuntos_Archivos_Adjunt~",
                        column: x => x.Archivos_AdjuntosId,
                        principalTable: "Archivos_Adjuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_Adjuntos_Relacion_Archivos_AdjuntosId",
                table: "Archivos_Adjuntos_Relacion",
                column: "Archivos_AdjuntosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archivos_Adjuntos_Relacion");

            migrationBuilder.DropTable(
                name: "Archivos_Adjuntos");
        }
    }
}
