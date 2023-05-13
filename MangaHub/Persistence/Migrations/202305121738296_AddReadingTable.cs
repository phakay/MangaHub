namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReadingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Readings",
                c => new
                    {
                        MangaId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MangaId, t.UserId })
                .ForeignKey("dbo.Mangas", t => t.MangaId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MangaId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Readings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Readings", "MangaId", "dbo.Mangas");
            DropIndex("dbo.Readings", new[] { "UserId" });
            DropIndex("dbo.Readings", new[] { "MangaId" });
            DropTable("dbo.Readings");
        }
    }
}
