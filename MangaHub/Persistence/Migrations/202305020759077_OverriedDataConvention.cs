namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverriedDataConvention : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mangas", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Mangas", new[] { "ArtistId" });
            AlterColumn("dbo.Mangas", "ArtistId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Mangas", "Name", c => c.String(maxLength: 255));
            CreateIndex("dbo.Mangas", "ArtistId");
            AddForeignKey("dbo.Mangas", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mangas", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Mangas", new[] { "ArtistId" });
            AlterColumn("dbo.Mangas", "Name", c => c.String());
            AlterColumn("dbo.Mangas", "ArtistId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Mangas", "ArtistId");
            AddForeignKey("dbo.Mangas", "ArtistId", "dbo.AspNetUsers", "Id");
        }
    }
}
