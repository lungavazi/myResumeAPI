using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myResumeAPI.Migrations
{
    public partial class AddFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkHistory_UserId",
                table: "WorkHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UserId",
                table: "Educations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactReferences_UserId",
                table: "ContactReferences",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactReferences_AboutMe_UserId",
                table: "ContactReferences",
                column: "UserId",
                principalTable: "AboutMe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AboutMe_UserId",
                table: "Educations",
                column: "UserId",
                principalTable: "AboutMe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHistory_AboutMe_UserId",
                table: "WorkHistory",
                column: "UserId",
                principalTable: "AboutMe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactReferences_AboutMe_UserId",
                table: "ContactReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AboutMe_UserId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkHistory_AboutMe_UserId",
                table: "WorkHistory");

            migrationBuilder.DropIndex(
                name: "IX_WorkHistory_UserId",
                table: "WorkHistory");

            migrationBuilder.DropIndex(
                name: "IX_Educations_UserId",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_ContactReferences_UserId",
                table: "ContactReferences");
        }
    }
}
