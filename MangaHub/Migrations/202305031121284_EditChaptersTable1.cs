namespace MangaHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditChaptersTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Chapters");
            DropPrimaryKey("dbo.Chapters");
            AddColumn("dbo.Chapters", "SerialNo", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Chapters", new[] { "SerialNo", "MangaId" });
            DropColumn("dbo.Chapters", "Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Chapters", "Number", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Chapters");
            DropColumn("dbo.Chapters", "SerialNo");
            AddPrimaryKey("dbo.Chapters", new[] { "Number", "MangaId" });
        }
    }
}
