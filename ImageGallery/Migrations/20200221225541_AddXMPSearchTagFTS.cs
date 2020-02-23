using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageGallery.Migrations
{
    public partial class AddXMPSearchTagFTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER file_insert;
                DROP TRIGGER file_delete;
                DROP TRIGGER file_update;

                CREATE VIRTUAL TABLE FileFTS_Migrate_AddXMPSearchTagFTS using fts5(Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts, XMPTags_fts, content='Files', content_rowid='Id', tokenize = ""unicode61 categories 'L* N* Co Z* P*' separators '|'""  );
                INSERT INTO FileFTS_Migrate_AddXMPSearchTagFTS(Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts) SELECT * FROM FileFTS;
                ALTER TABLE FileFTS RENAME TO FileFTS_DropForMigration;
                ALTER TABLE FileFTS_Migrate_AddXMPSearchTagFTS RENAME TO FileFTS;
                DROP TABLE FileFTS_DropForMigration;

                CREATE TRIGGER file_insert AFTER INSERT ON Files BEGIN
                  INSERT INTO FileFTS(rowid, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts, XMPTags_fts) VALUES (new.Id, new.Directory_fts, new.Name_fts, new.Name_tokenized_fts, new.Extension_fts, new.Custom_fts, new.XMPTags_fts);
                END;
                CREATE TRIGGER file_delete AFTER DELETE ON Files BEGIN
                  INSERT INTO FileFTS(FileFTS, rowid, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts, XMPTags_fts) VALUES ('delete', old.Id, old.Directory_fts, old.Name_fts, old.Name_tokenized_fts, old.Extension_fts, old.Custom_fts, old.XMPTags_fts);
                END;

                CREATE TRIGGER file_update AFTER UPDATE ON Files BEGIN
                  INSERT INTO FileFTS(FileFTS, rowid, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts, XMPTags_fts) VALUES ('delete', old.Id, old.Directory_fts, old.Name_fts, old.Name_tokenized_fts, old.Extension_fts, old.Custom_fts, old.XMPTags_fts);
                  INSERT INTO FileFTS(rowid, Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts, XMPTags_fts) VALUES (new.Id, new.Directory_fts, new.Name_fts, new.Name_tokenized_fts, new.Extension_fts, new.Custom_fts, new.XMPTags_fts);
                END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER file_insert;
                DROP TRIGGER file_delete;
                DROP TRIGGER file_update;
                CREATE VIRTUAL TABLE FileFTS_Migrate_AddXMPSearchTagFTS_Down using fts5(Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts, content='Files', content_rowid='Id', tokenize = ""unicode61 categories 'L* N* Co Z* P*' separators '|'""  );
                INSERT INTO FileFTS_Migrate_AddXMPSearchTagFTS_Down(Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts) SELECT Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts FROM FileFTS;
                ALTER TABLE FileFTS RENAME TO FileFTS_DropForMigration;
                ALTER TABLE FileFTS_Migrate_AddXMPSearchTagFTS_Down RENAME TO FileFTS;
                DROP TABLE FileFTS_DropForMigration;
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
