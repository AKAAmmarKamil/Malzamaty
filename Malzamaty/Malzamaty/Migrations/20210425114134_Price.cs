using Microsoft.EntityFrameworkCore.Migrations;

namespace Malzamaty.Migrations
{
    public partial class Price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "File");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "File",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Address",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Address",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "File");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "File",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
