using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnWebsite.Web.Migrations
{
    public partial class CourseUnitAndPages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlName",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    HtmlContent = table.Column<string>(nullable: true),
                    UnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -1,
                column: "Created",
                value: new DateTime(2020, 6, 18, 19, 2, 43, 34, DateTimeKind.Local).AddTicks(5846));

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "CourseId", "DisplayName" },
                values: new object[] { -1, -1, "Getting Started" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "HtmlContent", "Title", "UnitId" },
                values: new object[] { -1, "<h3>Welcome to our course!<h3><p>Let's begin by setting up our development envirnoment. We are going to use the Visual Studio 2019 IDE to develope our web application.</p>", "Setting Up", -1 });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_UnitId",
                table: "Pages",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CourseId",
                table: "Units",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropColumn(
                name: "UrlName",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -1,
                column: "Created",
                value: new DateTime(2020, 6, 18, 15, 45, 34, 133, DateTimeKind.Local).AddTicks(3086));
        }
    }
}
