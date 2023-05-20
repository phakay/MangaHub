using MangaHub.Core.Models;
using MangaHub.Migrations;
using MangaHub.Persistence;
using NUnit.Framework;
using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;

namespace MangaHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            var projectPath = Directory
                .GetParent(AppDomain.CurrentDomain.BaseDirectory)
                .Parent
                .Parent
                .FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(projectPath,"App_Data"));

            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();
            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser
            {
                UserName = "user1",
                Name = "user1", 
                Email = "-", 
                PasswordHash = "-"
            });

            context.Users.Add(new ApplicationUser
            {
                UserName = "user2",
                Name = "user2",
                Email = "-",
                PasswordHash = "-"
            });

            context.SaveChanges();


        }
    }
}
