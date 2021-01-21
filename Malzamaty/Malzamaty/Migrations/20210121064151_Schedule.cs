using Microsoft.EntityFrameworkCore.Migrations;

namespace Malzamaty.Migrations
{
    public partial class Schedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Subject_Su_ID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Users_St_ID",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "Su_ID",
                table: "Schedules",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "St_ID",
                table: "Schedules",
                newName: "SubjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_Su_ID",
                table: "Schedules",
                newName: "IX_Schedules_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_St_ID",
                table: "Schedules",
                newName: "IX_Schedules_SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Subject_SubjectID",
                table: "Schedules",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Users_UserID",
                table: "Schedules",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Subject_SubjectID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Users_UserID",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Schedules",
                newName: "Su_ID");

            migrationBuilder.RenameColumn(
                name: "SubjectID",
                table: "Schedules",
                newName: "St_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_UserID",
                table: "Schedules",
                newName: "IX_Schedules_Su_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_SubjectID",
                table: "Schedules",
                newName: "IX_Schedules_St_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Subject_Su_ID",
                table: "Schedules",
                column: "Su_ID",
                principalTable: "Subject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Users_St_ID",
                table: "Schedules",
                column: "St_ID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
