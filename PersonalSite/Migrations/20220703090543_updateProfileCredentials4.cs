using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSite.Migrations
{
    public partial class updateProfileCredentials4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoogleProfileEntity",
                columns: table => new
                {
                    SourceId = table.Column<string>(type: "text", nullable: false),
                    ProfileEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleProfileEntity", x => x.SourceId);
                    table.ForeignKey(
                        name: "FK_GoogleProfileEntity_Profiles_ProfileEntityId",
                        column: x => x.ProfileEntityId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoogleProfileEntity_ProfileEntityId",
                table: "GoogleProfileEntity",
                column: "ProfileEntityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoogleProfileEntity");
        }
    }
}
