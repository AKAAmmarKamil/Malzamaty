using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Malzamaty.Migrations
{
    public partial class Malzamaty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Co_ID = table.Column<string>(nullable: false),
                    Co_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Co_ID);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Su_ID = table.Column<string>(nullable: false),
                    Su_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Su_ID);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    C_ID = table.Column<string>(nullable: false),
                    C_Name = table.Column<string>(nullable: true),
                    C_Stage = table.Column<string>(nullable: true),
                    C_Type = table.Column<string>(nullable: true),
                    Co_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.C_ID);
                    table.ForeignKey(
                        name: "FK_Class_Country_Co_ID",
                        column: x => x.Co_ID,
                        principalTable: "Country",
                        principalColumn: "Co_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exist",
                columns: table => new
                {
                    E_ID = table.Column<string>(nullable: false),
                    C_ID = table.Column<string>(nullable: true),
                    Su_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exist", x => x.E_ID);
                    table.ForeignKey(
                        name: "FK_Exist_Class_C_ID",
                        column: x => x.C_ID,
                        principalTable: "Class",
                        principalColumn: "C_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exist_Subject_Su_ID",
                        column: x => x.Su_ID,
                        principalTable: "Subject",
                        principalColumn: "Su_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    St_ID = table.Column<string>(nullable: false),
                    St_FullName = table.Column<string>(nullable: true),
                    St_Email = table.Column<string>(nullable: true),
                    St_Password = table.Column<string>(nullable: true),
                    St_Authentication = table.Column<string>(nullable: true),
                    C_ID = table.Column<string>(nullable: true),
                    Su_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.St_ID);
                    table.ForeignKey(
                        name: "FK_Student_Class_C_ID",
                        column: x => x.C_ID,
                        principalTable: "Class",
                        principalColumn: "C_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Subject_Su_ID",
                        column: x => x.Su_ID,
                        principalTable: "Subject",
                        principalColumn: "Su_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    F_ID = table.Column<string>(nullable: false),
                    F_Description = table.Column<string>(nullable: true),
                    F_File = table.Column<byte[]>(nullable: true),
                    F_Author = table.Column<string>(nullable: true),
                    F_Type = table.Column<string>(nullable: true),
                    F_Format = table.Column<string>(nullable: true),
                    F_PublishDate = table.Column<DateTimeOffset>(nullable: false),
                    C_ID = table.Column<string>(nullable: true),
                    St_ID = table.Column<string>(nullable: true),
                    Su_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.F_ID);
                    table.ForeignKey(
                        name: "FK_File_Class_C_ID",
                        column: x => x.C_ID,
                        principalTable: "Class",
                        principalColumn: "C_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_File_Student_St_ID",
                        column: x => x.St_ID,
                        principalTable: "Student",
                        principalColumn: "St_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_File_Subject_Su_ID",
                        column: x => x.Su_ID,
                        principalTable: "Subject",
                        principalColumn: "Su_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Sc_ID = table.Column<string>(nullable: false),
                    StartStudy = table.Column<DateTime>(nullable: true),
                    FinishStudy = table.Column<DateTime>(nullable: true),
                    St_ID = table.Column<string>(nullable: true),
                    Su_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Sc_ID);
                    table.ForeignKey(
                        name: "FK_Schedules_Student_St_ID",
                        column: x => x.St_ID,
                        principalTable: "Student",
                        principalColumn: "St_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Subject_Su_ID",
                        column: x => x.Su_ID,
                        principalTable: "Subject",
                        principalColumn: "Su_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Ra_ID = table.Column<string>(nullable: false),
                    Ra_Comment = table.Column<string>(nullable: true),
                    Ra_Rate = table.Column<int>(nullable: false),
                    St_ID = table.Column<string>(nullable: true),
                    F_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Ra_ID);
                    table.ForeignKey(
                        name: "FK_Rating_File_F_ID",
                        column: x => x.F_ID,
                        principalTable: "File",
                        principalColumn: "F_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rating_Student_St_ID",
                        column: x => x.St_ID,
                        principalTable: "Student",
                        principalColumn: "St_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    R_ID = table.Column<string>(nullable: false),
                    R_Description = table.Column<string>(nullable: true),
                    R_Date = table.Column<DateTimeOffset>(nullable: false),
                    F_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.R_ID);
                    table.ForeignKey(
                        name: "FK_Report_File_F_ID",
                        column: x => x.F_ID,
                        principalTable: "File",
                        principalColumn: "F_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_Co_ID",
                table: "Class",
                column: "Co_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Exist_C_ID",
                table: "Exist",
                column: "C_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Exist_Su_ID",
                table: "Exist",
                column: "Su_ID");

            migrationBuilder.CreateIndex(
                name: "IX_File_C_ID",
                table: "File",
                column: "C_ID");

            migrationBuilder.CreateIndex(
                name: "IX_File_St_ID",
                table: "File",
                column: "St_ID");

            migrationBuilder.CreateIndex(
                name: "IX_File_Su_ID",
                table: "File",
                column: "Su_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_F_ID",
                table: "Rating",
                column: "F_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_St_ID",
                table: "Rating",
                column: "St_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_F_ID",
                table: "Report",
                column: "F_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_St_ID",
                table: "Schedules",
                column: "St_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Su_ID",
                table: "Schedules",
                column: "Su_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_C_ID",
                table: "Student",
                column: "C_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Su_ID",
                table: "Student",
                column: "Su_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exist");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
