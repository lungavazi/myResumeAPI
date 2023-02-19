using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myResumeAPI.Migrations
{
    public partial class Removefields_updateDatatypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDNumber",
                table: "AboutMe");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AboutMe");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AboutMe",
                type: "nvarchar(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AboutMe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDNumber",
                table: "AboutMe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AboutMe",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
