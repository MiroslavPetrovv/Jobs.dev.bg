using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplications.Migrations
{
    public partial class ApplicationCVAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvFilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvFilePath",
                table: "Applications");
        }
    }
}
