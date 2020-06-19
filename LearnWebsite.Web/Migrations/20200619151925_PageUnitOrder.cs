using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnWebsite.Web.Migrations
{
    public partial class PageUnitOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InCourseOrder",
                table: "Units",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InUnitOrder",
                table: "Pages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -1,
                column: "Created",
                value: new DateTime(2020, 6, 19, 18, 19, 23, 172, DateTimeKind.Local).AddTicks(5920));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InCourseOrder",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "InUnitOrder",
                table: "Pages");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -1,
                column: "Created",
                value: new DateTime(2020, 6, 18, 19, 2, 43, 34, DateTimeKind.Local).AddTicks(5846));
        }
    }
}
