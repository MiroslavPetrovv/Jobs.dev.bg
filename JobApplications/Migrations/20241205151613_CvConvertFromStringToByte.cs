using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplications.Migrations
{
   public partial class CvConvertFromStringToByte : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Create a temporary column to store converted data
        migrationBuilder.AddColumn<byte[]>(
            name: "CvFileBytes",
            table: "Applications",
            type: "varbinary(max)",
            nullable: true);

        // Use raw SQL to convert existing data from string to byte[]
        migrationBuilder.Sql(
            @"
            UPDATE Applications
            SET CvFileBytes = CONVERT(varbinary(max), CvFilePath)
            WHERE CvFilePath IS NOT NULL
            ");

        // Drop the old column
        migrationBuilder.DropColumn(name: "CvFilePath", table: "Applications");

        // Rename the temporary column to the original column name
        migrationBuilder.RenameColumn(
            name: "CvFileBytes",
            table: "Applications",
            newName: "CvFilePath");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Create a temporary column to store converted data
        migrationBuilder.AddColumn<string>(
            name: "CvFileString",
            table: "Applications",
            type: "nvarchar(max)",
            nullable: true);

        // Use raw SQL to convert existing data from byte[] to string
        migrationBuilder.Sql(
            @"
            UPDATE Applications
            SET CvFileString = CONVERT(nvarchar(max), CvFilePath)
            WHERE CvFilePath IS NOT NULL
            ");

        // Drop the old column
        migrationBuilder.DropColumn(name: "CvFilePath", table: "Applications");

        // Rename the temporary column to the original column name
        migrationBuilder.RenameColumn(
            name: "CvFileString",
            table: "Applications",
            newName: "CvFilePath");
    }
}
}
