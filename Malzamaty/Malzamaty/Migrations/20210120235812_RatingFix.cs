using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Malzamaty.Migrations
{
    public partial class RatingFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Users_Us_ID",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "F_ID",
                table: "Rating");

            migrationBuilder.RenameColumn(
                name: "Us_ID",
                table: "Rating",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_Us_ID",
                table: "Rating",
                newName: "IX_Rating_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Users_UserID",
                table: "Rating",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Users_UserID",
                table: "Rating");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Rating",
                newName: "Us_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_UserID",
                table: "Rating",
                newName: "IX_Rating_Us_ID");

            migrationBuilder.AddColumn<Guid>(
                name: "F_ID",
                table: "Rating",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Users_Us_ID",
                table: "Rating",
                column: "Us_ID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
