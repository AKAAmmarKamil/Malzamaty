using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Malzamaty.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Authentication = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Authentication",
                        column: x => x.Authentication,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    T_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Co_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Class_ClassType_T_ID",
                        column: x => x.T_ID,
                        principalTable: "ClassType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Class_Country_Co_ID",
                        column: x => x.Co_ID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Class_Stage_S_ID",
                        column: x => x.S_ID,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartStudy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishStudy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    St_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Su_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schedules_Subject_Su_ID",
                        column: x => x.Su_ID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Users_St_ID",
                        column: x => x.St_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    C_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Us_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Su_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.ID);
                    table.ForeignKey(
                        name: "FK_File_Class_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Class",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_File_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_File_Users_Us_ID",
                        column: x => x.Us_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    U_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    C_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Su_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Interests_Class_C_ID",
                        column: x => x.C_ID,
                        principalTable: "Class",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interests_Subject_Su_ID",
                        column: x => x.Su_ID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interests_Users_U_ID",
                        column: x => x.U_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    C_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Su_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Matches_Class_C_ID",
                        column: x => x.C_ID,
                        principalTable: "Class",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Subject_Su_ID",
                        column: x => x.Su_ID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Us_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    F_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rating_File_FileID",
                        column: x => x.FileID,
                        principalTable: "File",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rating_Users_Us_ID",
                        column: x => x.Us_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    F_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Report_File_F_ID",
                        column: x => x.F_ID,
                        principalTable: "File",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_Co_ID",
                table: "Class",
                column: "Co_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Class_S_ID",
                table: "Class",
                column: "S_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Class_T_ID",
                table: "Class",
                column: "T_ID");

            migrationBuilder.CreateIndex(
                name: "IX_File_ClassID",
                table: "File",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_File_SubjectID",
                table: "File",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_File_Us_ID",
                table: "File",
                column: "Us_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Interests_C_ID",
                table: "Interests",
                column: "C_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Interests_Su_ID",
                table: "Interests",
                column: "Su_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Interests_U_ID",
                table: "Interests",
                column: "U_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_C_ID",
                table: "Matches",
                column: "C_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Su_ID",
                table: "Matches",
                column: "Su_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_FileID",
                table: "Rating",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_Us_ID",
                table: "Rating",
                column: "Us_ID");

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
                name: "IX_Users_Authentication",
                table: "Users",
                column: "Authentication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ClassType");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
