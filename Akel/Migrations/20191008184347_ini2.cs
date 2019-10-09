using Microsoft.EntityFrameworkCore.Migrations;

namespace Akel.Migrations
{
    public partial class ini2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "qwerty",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "qwerty",
                table: "AspNetUsers");
        }
    }
}
