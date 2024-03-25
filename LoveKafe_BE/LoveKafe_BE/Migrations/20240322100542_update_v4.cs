using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveKafe_BE.Migrations
{
    public partial class update_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "category",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "category");
        }
    }
}
