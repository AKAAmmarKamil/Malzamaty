using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Malzamaty.Migrations
{
    public partial class Rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_File_FileID",
                table: "Report");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileID",
                table: "Report",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_File_FileID",
                table: "Report",
                column: "FileID",
                principalTable: "File",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_File_FileID",
                table: "Report");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileID",
                table: "Report",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_File_FileID",
                table: "Report",
                column: "FileID",
                principalTable: "File",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
