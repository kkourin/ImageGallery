using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageGallery.Migrations
{
    public partial class AddLastChangedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastChangeTime",
                table: "Files",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_LastChangeTime",
                table: "Files",
                column: "LastChangeTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Files_LastChangeTime",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "LastChangeTime",
                table: "Files");
        }
    }
}
