using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Malzamaty.Migrations
{
    public partial class File : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_AddressID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Library_AddressID",
                table: "Library");

            migrationBuilder.AddColumn<Guid>(
                name: "FileID",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID",
                unique: true,
                filter: "[AddressID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Order_FileID",
                table: "Order",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_Library_AddressID",
                table: "Library",
                column: "AddressID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_File_FileID",
                table: "Order",
                column: "FileID",
                principalTable: "File",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_File_FileID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Order_FileID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Library_AddressID",
                table: "Library");

            migrationBuilder.DropColumn(
                name: "FileID",
                table: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Library_AddressID",
                table: "Library",
                column: "AddressID");
        }
    }
}
