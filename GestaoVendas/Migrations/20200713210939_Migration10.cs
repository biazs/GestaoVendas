using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoVendas.Migrations
{
    public partial class Migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeFuncionalidade",
                table: "AcessoTipoUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeFuncionalidade",
                table: "AcessoTipoUsuario",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
