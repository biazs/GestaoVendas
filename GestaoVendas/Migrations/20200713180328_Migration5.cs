using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoVendas.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionalidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFuncionalidade = table.Column<string>(nullable: true),
                    IdAcessoTipoUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionalidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionalidade_AcessoTipoUsuario_IdAcessoTipoUsuario",
                        column: x => x.IdAcessoTipoUsuario,
                        principalTable: "AcessoTipoUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Funcionalidade_IdAcessoTipoUsuario",
                table: "Funcionalidade",
                column: "IdAcessoTipoUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Funcionalidade");

        }
    }
}
