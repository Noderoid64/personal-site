using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSite.Migrations
{
    public partial class addRefreshToken2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "ProfileCredentials");

            migrationBuilder.DropColumn(
                name: "RefreshTokenValidDateTime",
                table: "ProfileCredentials");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "GoogleProfiles");

            migrationBuilder.DropColumn(
                name: "RefreshTokenValidDateTime",
                table: "GoogleProfiles");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Profiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireOn",
                table: "Profiles",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireOn",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "ProfileCredentials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenValidDateTime",
                table: "ProfileCredentials",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
