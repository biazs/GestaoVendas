using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoVendas.Migrations
{
    public partial class Migration11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vendedores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vendedores");
        }
    }
}
