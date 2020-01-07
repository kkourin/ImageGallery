using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageGallery.Migrations
{
    public partial class AddFTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIRTUAL TABLE FileFTS using fts5(Directory_fts, Name_fts, Name_tokenized_fts, Extension_fts, Custom_fts, content='Files', content_rowid='Id', tokenize = ""unicode61 categories 'L* N* Co Z* P*' separators '|'""  );
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER file_insert");
            migrationBuilder.Sql("DROP TRIGGER file_delete");
            migrationBuilder.Sql("DROP TRIGGER file_update");
            migrationBuilder.DropTable(name: "FileFTS");
        }
    }
}
