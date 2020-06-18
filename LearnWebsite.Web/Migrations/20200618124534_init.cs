using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnWebsite.Web.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Viewed = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ImageSrc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Created", "Description", "DisplayName", "ImageSrc", "Viewed" },
                values: new object[] { -1, new DateTime(2020, 6, 18, 15, 45, 34, 133, DateTimeKind.Local).AddTicks(3086), "Learn the basics of web development using asp.net core 3.1, ef core and bootstrap", "ASP.NET Core Mvc", "data:image/png;base64,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", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
