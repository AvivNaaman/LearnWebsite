using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnWebsite.Web.Migrations
{
    public partial class UrlNamesOfCoursePagesAndUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlName",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlName",
                table: "Pages",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -1,
                column: "Created",
                value: new DateTime(2020, 6, 19, 20, 13, 9, 999, DateTimeKind.Local).AddTicks(2911));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlName",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "UrlName",
                table: "Pages");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -1,
                column: "Created",
                value: new DateTime(2020, 6, 19, 18, 19, 23, 172, DateTimeKind.Local).AddTicks(5920));
        }
    }
}
