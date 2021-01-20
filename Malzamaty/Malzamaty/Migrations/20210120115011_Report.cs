using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Malzamaty.DAL.Migrations
{
    public partial class Report : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_File_F_ID",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_F_ID",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "F_ID",
                table: "Report");

            migrationBuilder.AddColumn<Guid>(
                name: "FileID",
                table: "Report",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_FileID",
                table: "Report",
                column: "FileID");

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

            migrationBuilder.DropIndex(
                name: "IX_Report_FileID",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "FileID",
                table: "Report");

            migrationBuilder.AddColumn<Guid>(
                name: "F_ID",
                table: "Report",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Report_F_ID",
                table: "Report",
                column: "F_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_File_F_ID",
                table: "Report",
                column: "F_ID",
                principalTable: "File",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
