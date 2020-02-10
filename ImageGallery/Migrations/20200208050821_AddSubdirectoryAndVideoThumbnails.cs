using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageGallery.Migrations
{
    public partial class AddSubdirectoryAndVideoThumbnails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GenerateVideoThumbnails",
                table: "Watchers",
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ScanSubdirectories",
                table: "Watchers",
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenerateVideoThumbnails",
                table: "Watchers");

            migrationBuilder.DropColumn(
                name: "ScanSubdirectories",
                table: "Watchers");
        }
    }
}
