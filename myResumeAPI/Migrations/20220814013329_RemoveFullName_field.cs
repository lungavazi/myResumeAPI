using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myResumeAPI.Migrations
{
    public partial class RemoveFullName_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AboutMe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AboutMe",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
