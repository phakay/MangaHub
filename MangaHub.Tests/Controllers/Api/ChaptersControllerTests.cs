using AutoMapper;
using FluentAssertions;
using MangaHub.App_Start;
using MangaHub.Controllers.Api;
using MangaHub.Core;
using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using MangaHub.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace MangaHub.Tests.Controllers.Api
{
    [TestClass]
    public class ChaptersControllerTests
    {
        private ChaptersController _controller;
        private Mock<IChapterRepository> _mockChapterRepository;
        private Mock<IMangaRepository> _mockMangaRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            _mockChapterRepository = new Mock<IChapterRepository>();
            _mockMangaRepository = new Mock<IMangaRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.SetupGet(u => u.ChapterRepo).Returns(_mockChapterRepository.Object);
            mockUnitOfWork.SetupGet(u => u.MangaRepo).Returns(_mockMangaRepository.Object);

            _controller = new ChaptersController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Add_ValidRequest_ShouldReturnOk()
        {
            var dto = new ChapterDto
            {
                MangaId = 1,
                ChapterNo = 1,
                Information = "test",
                NumberOfPages = 150
            };

            _mockMangaRepository.Setup(m => m.GetManga(dto.MangaId))
                .Returns(new Manga { Id = dto.MangaId, ArtistId = _userId });

            var result = _controller.Add(dto);

            result.Should().BeOfType<OkResult>();   
        }

        [TestMethod]
        public void Add_MangaArtistIsNotTheUserLoggedIn_ShouldReturnUnauthorized()
        {
            var dto = new ChapterDto
            {
                MangaId = 1,
                ChapterNo = 1,
                Information = "test",
                NumberOfPages = 150
            };

            var manga = new Manga { Id = dto.MangaId, ArtistId = _userId + 1 };

            _mockMangaRepository.Setup(m => m.GetManga(dto.MangaId))
                .Returns(manga);

            var result = _controller.Add(dto);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Add_MangaDoesNotExist_ShouldReturnNotFoundResult()
        {
            var dto = new ChapterDto
            {
                MangaId = 1,
                ChapterNo = 1,
                Information = "test",
                NumberOfPages = 150
            };

            var result = _controller.Add(dto);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Add_ChapterNumberAlreadyExistsForManga_BadRequest()
        {
            var dto = new ChapterDto
            {
                MangaId = 1,
                ChapterNo = 1,
                Information = "test",
                NumberOfPages = 150
            };

            _mockMangaRepository.Setup(m => m.GetManga(dto.MangaId))
                .Returns(new Manga { Id = dto.MangaId, ArtistId = _userId });

            _mockChapterRepository.Setup(c => c.GetChapterForManga(dto.MangaId, dto.ChapterNo))
                .Returns(new Chapter { MangaId = dto.MangaId, ChapterNo = dto.ChapterNo });

            var result = _controller.Add(dto);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }
    }
}
