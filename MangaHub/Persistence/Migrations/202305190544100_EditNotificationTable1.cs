namespace MangaHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNotificationTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Message", c => c.String(nullable: false));
            DropColumn("dbo.Notifications", "DataBefore");
            DropColumn("dbo.Notifications", "DataAfter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "DataAfter", c => c.Binary(nullable: false));
            AddColumn("dbo.Notifications", "DataBefore", c => c.Binary(nullable: false));
            DropColumn("dbo.Notifications", "Message");
        }
    }
}
