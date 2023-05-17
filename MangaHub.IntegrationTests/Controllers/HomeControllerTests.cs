using FluentAssertions;
using MangaHub.Controllers;
using MangaHub.Core.Models;
using MangaHub.Core.ViewModels;
using MangaHub.Extensions;
using MangaHub.Persistence;
using NUnit.Framework;
using System.Linq;

namespace MangaHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new HomeController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Index_WhenCallded_ShouldReturnListOfMangasWithChapters()
        {
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var manga = new Manga
            {
                Artist = user,
                Picture = new byte[0],
                Genre = _context.Genres.First(),
                Title = "-",
                Description = "-"
            };

            _context.Mangas.Add(manga);

            var chapter = new Chapter
            {
                Manga = manga,
                ChapterNo = 1,
                Information = "-",
                NumberOfPages = 150
            };

            _context.Chapters.Add(chapter);
            _context.SaveChanges();

            var result = _controller.Index()
                .ViewData.Model as MangasViewModel;

            result.Mangas.Should().HaveCount(1);
            result.Mangas.First().Chapters.Should().HaveCount(1);

        }
    }
}
