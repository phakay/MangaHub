using FluentAssertions;
using MangaHub.Core.Models;
using MangaHub.Persistence;
using MangaHub.Persistence.Repositories;
using MangaHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace MangaHub.Tests.Repositories
{
    [TestClass]
    public class MangaRepositoryTests
    {
        private MangaRepository _repo;
        private Mock<DbSet<Manga>> _mockDbSet;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockDbSet = new Mock<DbSet<Manga>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Mangas)
                .Returns(_mockDbSet.Object);

            _repo = new MangaRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetManga_ValidMangaId_MangaShouldBeReturned()
        {
            var manga = new Manga { Id = 1};
            _mockDbSet.SetSource(new[] { manga } );

            var result = _repo.GetManga(manga.Id);

            (result.Id == manga.Id).Should().BeTrue();
        }
    }
}
