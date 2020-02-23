using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageGallery.Migrations
{
    public partial class AddXMPSearchTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "XMPTags_fts",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER file_insert;
                DROP TRIGGER file_delete;
                DROP TRIGGER file_update;

                ALTER TABLE Files RENAME TO _Files_old;
                CREATE TABLE ""Files"" (
                    ""Id"" INTEGER NOT NULL CONSTRAINT ""PK_Files"" PRIMARY KEY AUTOINCREMENT,
                    ""FullName"" TEXT NOT NULL,
                    ""Directory"" TEXT NOT NULL,
                    ""Name"" TEXT NOT NULL,
                    ""Extension"" TEXT NOT NULL,
                    ""CreatedTime"" TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
                    ""FileCreatedTime"" TEXT NULL,
                    ""LastUseTime"" TEXT NULL,
                    ""Thumbnail"" BLOB NULL,
                    ""TimesAccessed"" INTEGER NOT NULL DEFAULT 0,
                    ""Comment"" TEXT NOT NULL DEFAULT '',
                    ""Directory_fts"" TEXT NOT NULL DEFAULT '',
                    ""Name_fts"" TEXT NOT NULL DEFAULT '',
                    ""Name_tokenized_fts"" TEXT NOT NULL DEFAULT '',
                    ""Extension_fts"" TEXT NOT NULL DEFAULT '',
                    ""Custom_fts"" TEXT NOT NULL DEFAULT '',
                    ""WatcherId"" INTEGER NOT NULL, ""FileModifiedTime"" TEXT NULL, ""LastChangeTime"" TEXT NULL,
                    CONSTRAINT ""FK_Files_Watchers_WatcherId"" FOREIGN KEY (""WatcherId"") REFERENCES ""Watchers"" (""Id"") ON DELETE CASCADE
                );
                INSERT INTO Files (Id, FullName, Directory, Name, Extension, CreatedTime, FileCreatedTime, LastUseTime, Thumbnail, TimesAccessed, Comment, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts, WatcherId, FileModifiedTime, LastChangeTime) SELECT 
                    Id, FullName, Directory, Name, Extension, CreatedTime, FileCreatedTime, LastUseTime, Thumbnail, TimesAccessed, Comment, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts, WatcherId, FileModifiedTime, LastChangeTime from _Files_old;
                DROP TABLE _Files_old;

                CREATE INDEX ""IX_Files_CreatedTime"" ON ""Files"" (""CreatedTime"");

                CREATE INDEX ""IX_Files_Directory"" ON ""Files"" (""Directory"");

                CREATE INDEX ""IX_Files_FileCreatedTime"" ON ""Files"" (""FileCreatedTime"");

                CREATE INDEX ""IX_Files_FullName"" ON ""Files"" (""FullName"");

                CREATE INDEX ""IX_Files_LastUseTime"" ON ""Files"" (""LastUseTime"");

                CREATE INDEX ""IX_Files_TimesAccessed"" ON ""Files"" (""TimesAccessed"");

                CREATE INDEX ""IX_Files_WatcherId"" ON ""Files"" (""WatcherId"");

                CREATE UNIQUE INDEX ""IX_Files_FullName_WatcherId"" ON ""Files"" (""FullName"", ""WatcherId"");

                CREATE INDEX ""IX_Files_FileModifiedTime"" ON ""Files"" (""FileModifiedTime"");


                CREATE INDEX ""IX_Files_LastChangeTime"" ON ""Files""(""LastChangeTime"");


                CREATE TRIGGER file_insert AFTER INSERT ON Files BEGIN
                  INSERT INTO FileFTS(rowid, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts) VALUES (new.Id, new.Directory_fts, new.Name_fts, new.Name_tokenized_fts, new.Extension_fts, new.Custom_fts);
                END;
                CREATE TRIGGER file_delete AFTER DELETE ON Files BEGIN
                  INSERT INTO FileFTS(FileFTS, rowid, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts) VALUES ('delete', old.Id, old.Directory_fts, old.Name_fts, old.Name_tokenized_fts, old.Extension_fts, old.Custom_fts);
                END;

                CREATE TRIGGER file_update AFTER UPDATE ON Files BEGIN
                  INSERT INTO FileFTS(FileFTS, rowid, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts) VALUES ('delete', old.Id, old.Directory_fts, old.Name_fts, old.Name_tokenized_fts, old.Extension_fts, old.Custom_fts);
                  INSERT INTO FileFTS(rowid, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts) VALUES (new.Id, new.Directory_fts, new.Name_fts, new.Name_tokenized_fts, new.Extension_fts, new.Custom_fts);
                END;
            ");
        }
    }
}
