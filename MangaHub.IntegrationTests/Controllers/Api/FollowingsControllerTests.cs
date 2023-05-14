using AutoMapper;
using FluentAssertions;
using MangaHub.App_Start;
using MangaHub.Controllers.Api;
using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using MangaHub.Extensions;
using MangaHub.Persistence;
using NUnit.Framework;

namespace MangaHub.IntegrationTests.Controllers.Api
{
    [TestFixture]
    public class FollowingsControllerTests
    {
        private ApplicationDbContext _context;
        private FollowingsController _controller;

        [SetUp]
        public void SetUp()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            _context = new ApplicationDbContext();
            _controller = new FollowingsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Follow_ValidRequest_TheGivenFollowingshouldBeAddedToDatabase()
        {
            var artist = new ApplicationUser
            {
                Name = "artist1",
                UserName = "artist1@test.com"
     
            };

            var reader = new ApplicationUser
            {
                Name = "reader1",
                UserName = "reader1@test.com"
            };

            _context.Users.Add(artist);
            _context.Users.Add(reader);
            _context.SaveChanges();

            var dto = new FollowingDto
            {
                FolloweeId = artist.Id
            };

            _controller.MockCurrentUser(reader.Id, reader.UserName);

            _controller.Follow(dto);

            var result = _context.Followings.Find(artist.Id, reader.Id);

            result.Should().NotBeNull();
        }

        [Test, Isolated]
        public void Unfollow_ValidRequest_TheGivenFollowingshouldBeRemoved()
        {
            var artist = new ApplicationUser
            {
                Id = "4",
                Name = "artist1",
                UserName = "artist1@test.com"

            };

            var reader = new ApplicationUser
            {
                Id = "5",
                Name = "reader1",
                UserName = "reader1@test.com"
            };

            var following = new Following
            {
                FolloweeId = artist.Id,
                FollowerId = reader.Id
            };

            _context.Users.Add(artist);
            _context.Users.Add(reader);
            _context.Followings.Add(following);
            _context.SaveChanges();

            _controller.MockCurrentUser(reader.Id, reader.UserName);

            _controller.Unfollow(artist.Id);

            var result = _context.Followings.Find(artist.Id, reader.Id);

            result.Should().BeNull();
        }
    }
}
