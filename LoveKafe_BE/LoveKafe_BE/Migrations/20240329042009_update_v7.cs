using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveKafe_BE.Migrations
{
    public partial class update_v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "userDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "userDetail");
        }
    }
}
