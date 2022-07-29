using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSite.Migrations
{
    public partial class addRefreshToken1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "GoogleProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenValidDateTime",
                table: "GoogleProfiles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "GoogleProfiles");

            migrationBuilder.DropColumn(
                name: "RefreshTokenValidDateTime",
                table: "GoogleProfiles");
        }
    }
}
