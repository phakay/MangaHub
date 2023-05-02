namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMangaTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mangas", "Title", c => c.String(maxLength: 255));
            DropColumn("dbo.Mangas", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mangas", "Name", c => c.String(maxLength: 255));
            DropColumn("dbo.Mangas", "Title");
        }
    }
}
