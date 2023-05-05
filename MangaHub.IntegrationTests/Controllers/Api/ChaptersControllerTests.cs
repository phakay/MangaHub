using FluentAssertions;
using MangaHub.Controllers.Api;
using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using MangaHub.Extensions;
using MangaHub.Persistence;
using NUnit.Framework;
using System;
using System.Linq;

namespace MangaHub.IntegrationTests.Controllers.Api
{
    [TestFixture]
    public class ChaptersControllerTests
    {
        private ApplicationDbContext _context;
        private ChaptersController _controller;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new ChaptersController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Add_ValidRequest_TheGivenChapterShouldBeAddedToDatabase()
        {
            var user = _context.Users.First();
            var manga = new Manga
            {
                Artist = user,
                Picture = new byte[0],
                GenreId = 1,
                DateCreated = DateTime.Now,
                Title = "-",
                Description = "-"
            };

            _context.Mangas.Add(manga);
            _context.SaveChanges();

            var dto = new ChapterDto
            {
                MangaId = manga.Id,
                ChapterNo = 1,
                Information = "-",
                NumberOfPages = 200
            };

            _controller.MockCurrentUser(user.Id, user.UserName);

            _controller.Add(dto);

            var result = _context.Chapters
                .SingleOrDefault(c => c.MangaId == manga.Id && 
                c.ChapterNo == dto.ChapterNo);

            result.Should().NotBeNull();
        }
    }
}
