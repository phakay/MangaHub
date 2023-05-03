namespace MangaHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditManagaTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Mangas SET Description = '' WHERE Description IS NULL");
            AlterColumn("dbo.Mangas", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Mangas", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Mangas", "Picture", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mangas", "Picture", c => c.Binary());
            AlterColumn("dbo.Mangas", "Description", c => c.String());
            AlterColumn("dbo.Mangas", "Title", c => c.String(maxLength: 255));
        }
    }
}
