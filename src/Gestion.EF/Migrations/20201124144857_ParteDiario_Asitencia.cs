using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion.EF.Migrations
{
    public partial class ParteDiario_Asitencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParteDiario_Asistencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParteDiarioId = table.Column<int>(nullable: false),
                    ContratistasId = table.Column<int>(nullable: false),
                    Asig_Propios = table.Column<int>(nullable: false),
                    Asig_Propios_Presentes = table.Column<int>(nullable: false),
                    Asig_Contratista = table.Column<int>(nullable: false),
                    Asig_Contratista_Presentes = table.Column<int>(nullable: false),
                    Asig_Comentario = table.Column<string>(nullable: true),
                    Covid_Propioa = table.Column<int>(nullable: false),
                    Covid_Contratista = table.Column<int>(nullable: false),
                    Covid_Comentario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParteDiario_Asistencia", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParteDiario_Asistencia");
        }
    }
}
