using FluentAssertions;
using MangaHub.Controllers;
using MangaHub.Core;
using MangaHub.Core.Models;
using MangaHub.Core.ViewModels;
using MangaHub.Extensions;
using MangaHub.Persistence;
using NUnit.Framework;
using System.Linq;

namespace MangaHub.IntegrationTests.Controllers
{
    [TestFixture]
    class MangaControllerTests
    {
        private MangasController _controller;
        private IUnitOfWork _unitOfWork;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _controller = new MangasController(_unitOfWork);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Create_ValidRequest_NewMangaIsCreated()
        {
            // Arrange
            var genre = _context.Genres.First();
            var user = _context.Users.First();
            var viewModel = new MangaFormViewModel
            {
              Title = "-",
              Description = "-",
              Genre = genre.Id
            };
            _controller.MockCurrentUser(user.Id, user.UserName);

            //Act
            _controller.Create(viewModel);

            //Assert
            _context.Mangas
                .SingleOrDefault(m => m.Title == viewModel.Title && 
                m.Description == viewModel.Description &&
                m.GenreId == viewModel.Genre &&
                m.Artist.Id == user.Id)
                .Should().NotBeNull();
        }

        [Test, Isolated]
        public void Create_ValidRequest_NotificationIsCreatedFollowersOfArtist()
        {
            // Arrange
            var genre = _context.Genres.First();
            var user = _context.Users.First();
            var follower = _context.Users.Single(u => u.Id != user.Id);
            user.Followers.Add(new Following(follower, user));

            var viewModel = new MangaFormViewModel
            {
                Title = "-",
                Description = "-",
                Genre = genre.Id
            };
            _controller.MockCurrentUser(user.Id, user.UserName);

            //Act
            _controller.Create(viewModel);

            //Assert
            _context.UserNotifications
                .SingleOrDefault(un => un.UserId == follower.Id && !un.IsRead)
                .Should().NotBeNull();
        }
    }
}
