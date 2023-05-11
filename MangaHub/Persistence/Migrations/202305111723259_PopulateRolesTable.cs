namespace MangaHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateRolesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES ('1', 'Artist')");
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES ('2', 'Reader')");
        }
        
        public override void Down()
        {
            Sql("DELETE AspNetRoles WHERE Id IN ('1', '2')");
        }
    }
}
