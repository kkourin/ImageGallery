using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageGallery.Migrations
{
    public partial class AddShortcutColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "GlobalShortcut",
                table: "Watchers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ShortcutKeys",
                table: "Watchers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalShortcut",
                table: "Watchers");

            migrationBuilder.DropColumn(
                name: "ShortcutKeys",
                table: "Watchers");
        }
    }
}
