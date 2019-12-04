using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akel.Migrations.Appl
{
    public partial class addAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "UserProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "UserProfiles");
        }
    }
}
