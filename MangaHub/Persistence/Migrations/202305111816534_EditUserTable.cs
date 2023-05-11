namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditUserTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "IsArtist");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsArtist", c => c.Boolean(nullable: false));
        }
    }
}
