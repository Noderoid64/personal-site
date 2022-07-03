using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonalSite.Migrations
{
    public partial class updateProfileCredentials3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_ProfileCredentials_Id",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_ProfileCredentials_UserId",
                table: "ProfileCredentials");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Profiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCredentials_UserId",
                table: "ProfileCredentials",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProfileCredentials_UserId",
                table: "ProfileCredentials");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Profiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCredentials_UserId",
                table: "ProfileCredentials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_ProfileCredentials_Id",
                table: "Profiles",
                column: "Id",
                principalTable: "ProfileCredentials",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
