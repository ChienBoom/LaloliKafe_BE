using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveKafe_BE.Migrations
{
    public partial class update_v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "userDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "table",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "table",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "orderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "orderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "area",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "userDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "table");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "table");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "product");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "orderDetail");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "orderDetail");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "order");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "order");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "order");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "category");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "area");
        }
    }
}
