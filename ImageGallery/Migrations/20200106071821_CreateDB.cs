using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageGallery.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Watchers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Directory = table.Column<string>(nullable: false),
                    Whitelist = table.Column<string>(nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(nullable: false),
                    Directory = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FileCreatedTime = table.Column<DateTime>(nullable: true),
                    LastUseTime = table.Column<DateTime>(nullable: true),
                    Thumbnail = table.Column<byte[]>(nullable: true),
                    TimesAccessed = table.Column<int>(nullable: false, defaultValue: 0),
                    Comment = table.Column<string>(nullable: false, defaultValue: ""),
                    Directory_fts = table.Column<string>(nullable: false, defaultValue: ""),
                    Name_fts = table.Column<string>(nullable: false, defaultValue: ""),
                    Name_tokenized_fts = table.Column<string>(nullable: false, defaultValue: ""),
                    Extension_fts = table.Column<string>(nullable: false, defaultValue: ""),
                    Custom_fts = table.Column<string>(nullable: false, defaultValue: ""),
                    WatcherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Watchers_WatcherId",
                        column: x => x.WatcherId,
                        principalTable: "Watchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_CreatedTime",
                table: "Files",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Files_Directory",
                table: "Files",
                column: "Directory");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileCreatedTime",
                table: "Files",
                column: "FileCreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FullName",
                table: "Files",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_Files_LastUseTime",
                table: "Files",
                column: "LastUseTime");

            migrationBuilder.CreateIndex(
                name: "IX_Files_TimesAccessed",
                table: "Files",
                column: "TimesAccessed");

            migrationBuilder.CreateIndex(
                name: "IX_Files_WatcherId",
                table: "Files",
                column: "WatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FullName_WatcherId",
                table: "Files",
                columns: new[] { "FullName", "WatcherId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Watchers");
        }
    }
}
