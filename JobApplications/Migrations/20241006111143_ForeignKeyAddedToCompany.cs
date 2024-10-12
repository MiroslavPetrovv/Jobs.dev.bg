using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplications.Migrations
{
    public partial class ForeignKeyAddedToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_IdentityUserId",
                table: "Companies",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_IdentityUserId",
                table: "Companies",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_IdentityUserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_IdentityUserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Companies");
        }
    }
}
