namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMangaTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mangas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArtistId = c.String(maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        GenreId = c.Byte(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Picture = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mangas", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Mangas", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Mangas", new[] { "GenreId" });
            DropIndex("dbo.Mangas", new[] { "ArtistId" });
            DropTable("dbo.Mangas");
            DropTable("dbo.Genres");
        }
    }
}
