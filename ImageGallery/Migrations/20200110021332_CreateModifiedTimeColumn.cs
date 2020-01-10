using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageGallery.Migrations
{
    public partial class CreateModifiedTimeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FileModifiedTime",
                table: "Files",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileModifiedTime",
                table: "Files",
                column: "FileModifiedTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Files_FileModifiedTime",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileModifiedTime",
                table: "Files");
        }
    }
}
