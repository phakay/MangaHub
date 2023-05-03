namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditChaptersTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapters", "Information", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chapters", "Information");
        }
    }
}
