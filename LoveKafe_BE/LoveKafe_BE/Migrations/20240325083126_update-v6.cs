using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveKafe_BE.Migrations
{
    public partial class updatev6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "userDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "userDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "userDetail");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "userDetail");
        }
    }
}
