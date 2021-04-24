using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Malzamaty.Migrations
{
    public partial class Admins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressID",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressID",
                table: "Users",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressID",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressID",
                table: "Users",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
