using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSite.Migrations
{
    public partial class updateProfileCredentials5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoogleProfileEntity_Profiles_ProfileEntityId",
                table: "GoogleProfileEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoogleProfileEntity",
                table: "GoogleProfileEntity");

            migrationBuilder.RenameTable(
                name: "GoogleProfileEntity",
                newName: "GoogleProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_GoogleProfileEntity_ProfileEntityId",
                table: "GoogleProfiles",
                newName: "IX_GoogleProfiles_ProfileEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoogleProfiles",
                table: "GoogleProfiles",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoogleProfiles_Profiles_ProfileEntityId",
                table: "GoogleProfiles",
                column: "ProfileEntityId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoogleProfiles_Profiles_ProfileEntityId",
                table: "GoogleProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoogleProfiles",
                table: "GoogleProfiles");

            migrationBuilder.RenameTable(
                name: "GoogleProfiles",
                newName: "GoogleProfileEntity");

            migrationBuilder.RenameIndex(
                name: "IX_GoogleProfiles_ProfileEntityId",
                table: "GoogleProfileEntity",
                newName: "IX_GoogleProfileEntity_ProfileEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoogleProfileEntity",
                table: "GoogleProfileEntity",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoogleProfileEntity_Profiles_ProfileEntityId",
                table: "GoogleProfileEntity",
                column: "ProfileEntityId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
