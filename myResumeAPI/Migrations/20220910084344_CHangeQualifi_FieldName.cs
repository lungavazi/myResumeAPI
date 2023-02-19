using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myResumeAPI.Migrations
{
    public partial class CHangeQualifi_FieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QualilificationName",
                table: "Educations",
                newName: "QualificationName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QualificationName",
                table: "Educations",
                newName: "QualilificationName");
        }
    }
}
