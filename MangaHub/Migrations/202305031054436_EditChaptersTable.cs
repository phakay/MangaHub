namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditChaptersTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Chapters");
            AddPrimaryKey("dbo.Chapters", new[] { "Number", "MangaId" });
            DropColumn("dbo.Chapters", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Chapters", "Id", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Chapters");
            AddPrimaryKey("dbo.Chapters", new[] { "Id", "MangaId", "Number" });
        }
    }
}
