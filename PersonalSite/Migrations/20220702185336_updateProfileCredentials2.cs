using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonalSite.Migrations
{
    public partial class updateProfileCredentials2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileCredentialsEntity_Profiles_UserId",
                table: "ProfileCredentialsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileCredentialsEntity",
                table: "ProfileCredentialsEntity");

            migrationBuilder.RenameTable(
                name: "ProfileCredentialsEntity",
                newName: "ProfileCredentials");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Profiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "ProfileCredentials",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileCredentials",
                table: "ProfileCredentials",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCredentials_UserId",
                table: "ProfileCredentials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileCredentials_Profiles_UserId",
                table: "ProfileCredentials",
                column: "UserId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_ProfileCredentials_Id",
                table: "Profiles",
                column: "Id",
                principalTable: "ProfileCredentials",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileCredentials_Profiles_UserId",
                table: "ProfileCredentials");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_ProfileCredentials_Id",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileCredentials",
                table: "ProfileCredentials");

            migrationBuilder.DropIndex(
                name: "IX_ProfileCredentials_UserId",
                table: "ProfileCredentials");

            migrationBuilder.DropColumn(
                name: "id",
                table: "ProfileCredentials");

            migrationBuilder.RenameTable(
                name: "ProfileCredentials",
                newName: "ProfileCredentialsEntity");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Profiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileCredentialsEntity",
                table: "ProfileCredentialsEntity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileCredentialsEntity_Profiles_UserId",
                table: "ProfileCredentialsEntity",
                column: "UserId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
