using FluentAssertions;
using MangaHub.Core.Models;
using MangaHub.Extensions;
using MangaHub.Persistence;
using MangaHub.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace MangaHub.Tests.Repositories
{
    [TestClass]
    public class ChapterRepositoryTests
    {
        private Mock<DbSet<Chapter>> _mockDbSet;
        private ChapterRepository _repo;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDbSet = new Mock<DbSet<Chapter>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Chapters)
                .Returns(_mockDbSet.Object);

            _repo = new ChapterRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetChapterForManga_ChapterNumberDoesNotExistForManga_ShouldReturnChapter()
        {
            var request = new { mangaId = 1, chapterNo = 1 };
            var chapter = new Chapter { MangaId = request.mangaId, ChapterNo = request.chapterNo };
            _mockDbSet.SetSource(new[] { chapter });

            var result = _repo.GetChapterForManga(request.mangaId, request.chapterNo);

            result.Should().NotBeNull();
        }
        [TestMethod]
        public void GetChapterForManga_ChapterNumberDoesNotExistsForManga_ShouldReturnNull()
        {
            var request = new { mangaId = 1, chapterNo = 1 };
            var chapter = new Chapter { MangaId = request.mangaId, ChapterNo = request.chapterNo + 1 };
            _mockDbSet.SetSource(new[] { chapter });

            var result = _repo.GetChapterForManga(request.mangaId, request.chapterNo);

            result.Should().BeNull();
        }
    }
}
