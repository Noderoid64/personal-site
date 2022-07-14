using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSite.Migrations
{
    public partial class RenamePostEntityToFileObjectEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EditedAt",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "FileObjectType",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ParentId",
                table: "Posts",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_ParentId",
                table: "Posts",
                column: "ParentId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_ParentId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ParentId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "FileObjectType",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditedAt",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
