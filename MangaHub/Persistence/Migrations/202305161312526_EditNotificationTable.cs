namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNotificationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "DateCreated");
        }
    }
}
