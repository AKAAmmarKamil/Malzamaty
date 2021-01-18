using Microsoft.EntityFrameworkCore.Migrations;

namespace Malzamaty.DAL.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Format",
                table: "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "File",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
