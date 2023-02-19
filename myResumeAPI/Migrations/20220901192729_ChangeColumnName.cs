using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myResumeAPI.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ContactReferences_AboutMe_AboutMeID",
            //    table: "ContactReferences");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Educations_AboutMe_AboutMeID",
            //    table: "Educations");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkHistory_AboutMe_AboutMeID",
            //    table: "WorkHistory");

            //migrationBuilder.DropIndex(
            //    name: "IX_WorkHistory_AboutMeID",
            //    table: "WorkHistory");

            //migrationBuilder.DropIndex(
            //    name: "IX_Educations_AboutMeID",
            //    table: "Educations");

            //migrationBuilder.DropIndex(
            //    name: "IX_ContactReferences_AboutMeID",
            //    table: "ContactReferences");

            //migrationBuilder.DropColumn(
            //    name: "AboutMeID",
            //    table: "WorkHistory");

            //migrationBuilder.DropColumn(
            //    name: "AboutMeID",
            //    table: "Educations");

            //migrationBuilder.DropColumn(
            //    name: "AboutMeID",
            //    table: "ContactReferences");

            migrationBuilder.RenameColumn(
                name: "RefTitle",
                table: "ContactReferences",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "RefTelephone",
                table: "ContactReferences",
                newName: "Telephone");

            migrationBuilder.RenameColumn(
                name: "RefName",
                table: "ContactReferences",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "RefEmailAddre",
                table: "ContactReferences",
                newName: "EmailAddre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ContactReferences",
                newName: "RefTitle");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                table: "ContactReferences",
                newName: "RefTelephone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ContactReferences",
                newName: "RefName");

            migrationBuilder.RenameColumn(
                name: "EmailAddre",
                table: "ContactReferences",
                newName: "RefEmailAddre");

            migrationBuilder.AddColumn<long>(
                name: "AboutMeID",
                table: "WorkHistory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AboutMeID",
                table: "Educations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AboutMeID",
                table: "ContactReferences",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistory_AboutMeID",
                table: "WorkHistory",
                column: "AboutMeID");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_AboutMeID",
                table: "Educations",
                column: "AboutMeID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactReferences_AboutMeID",
                table: "ContactReferences",
                column: "AboutMeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactReferences_AboutMe_AboutMeID",
                table: "ContactReferences",
                column: "AboutMeID",
                principalTable: "AboutMe",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AboutMe_AboutMeID",
                table: "Educations",
                column: "AboutMeID",
                principalTable: "AboutMe",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHistory_AboutMe_AboutMeID",
                table: "WorkHistory",
                column: "AboutMeID",
                principalTable: "AboutMe",
                principalColumn: "ID");
        }
    }
}
