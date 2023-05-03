namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditChaptersTable3 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Chapters");
            DropPrimaryKey("dbo.Chapters");
            AddColumn("dbo.Chapters", "ChapterNo", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Chapters", new[] { "ChapterNo", "MangaId" });
            DropColumn("dbo.Chapters", "SerialNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Chapters", "SerialNo", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Chapters");
            DropColumn("dbo.Chapters", "ChapterNo");
            AddPrimaryKey("dbo.Chapters", new[] { "SerialNo", "MangaId" });
        }
    }
}
