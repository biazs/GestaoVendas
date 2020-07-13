using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoVendas.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdFuncionalidade",
                table: "AcessoTipoUsuario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Funcionalidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFuncionalidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionalidade", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcessoTipoUsuario_IdFuncionalidade",
                table: "AcessoTipoUsuario",
                column: "IdFuncionalidade");

            migrationBuilder.AddForeignKey(
                name: "FK_AcessoTipoUsuario_Funcionalidade_IdFuncionalidade",
                table: "AcessoTipoUsuario",
                column: "IdFuncionalidade",
                principalTable: "Funcionalidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcessoTipoUsuario_Funcionalidade_IdFuncionalidade",
                table: "AcessoTipoUsuario");

            migrationBuilder.DropTable(
                name: "Funcionalidade");

            migrationBuilder.DropIndex(
                name: "IX_AcessoTipoUsuario_IdFuncionalidade",
                table: "AcessoTipoUsuario");

            migrationBuilder.DropColumn(
                name: "IdFuncionalidade",
                table: "AcessoTipoUsuario");
        }
    }
}
