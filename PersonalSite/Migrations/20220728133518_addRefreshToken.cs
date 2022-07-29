using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSite.Migrations
{
    public partial class addRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "ProfileCredentials");

            migrationBuilder.DropColumn(
                name: "RefreshTokenValidDateTime",
                table: "ProfileCredentials");
        }
    }
}
