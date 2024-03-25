using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveKafe_BE.Migrations
{
    public partial class update_v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "userDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "category",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "userDetail");

            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "product");

            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "category");
        }
    }
}
