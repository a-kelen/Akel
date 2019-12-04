using Microsoft.EntityFrameworkCore.Migrations;

namespace Akel.Migrations.Appl
{
    public partial class addPhotoAud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Auditions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Auditions");
        }
    }
}
