namespace MangaHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Adventure')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Horror')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Comedy')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id in (1,2,3,4)");
        }
    }
}
