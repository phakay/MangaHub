namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChaptersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chapters",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MangaId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        NumberOfPages = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.MangaId, t.Number })
                .ForeignKey("dbo.Mangas", t => t.MangaId, cascadeDelete: true)
                .Index(t => t.MangaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chapters", "MangaId", "dbo.Mangas");
            DropIndex("dbo.Chapters", new[] { "MangaId" });
            DropTable("dbo.Chapters");
        }
    }
}
