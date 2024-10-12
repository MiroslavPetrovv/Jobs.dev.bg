using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplications.Migrations
{
    public partial class IndustriesDataEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Industries",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Industry focused on technology and innovation", "Technology" },
                    { 2, "Industry focused on health services and products", "Healthcare" },
                    { 3, "Industry involved in production and manufacturing", "Manufacturing" },
                    { 4, "Industry dealing with finance and investment", "Finance" },
                    { 5, "Industry related to education and training", "Education" },
                    { 6, "Industry focused on selling goods and services", "Retail" },
                    { 7, "Industry related to building and construction", "Construction" },
                    { 8, "Industry involving the movement of goods and people", "Transportation" },
                    { 9, "Industry related to services in hotels, restaurants, and tourism", "Hospitality" },
                    { 10, "Industry related to farming and food production", "Agriculture" },
                    { 30, "Industry focused on sustainability and environmental protection", "Environmental Services" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValue: 30);
        }
    }
}
