using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using MangaHub.Persistence;
using MangaHub.Persistence.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace MangaHub.Controllers.Api
{
    public class ChaptersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly MangaRepository _mangaRepo;
        private readonly ChapterRepository _chapterRepo;

        public ChaptersController()
        {
            _context = new ApplicationDbContext();
            _mangaRepo = new MangaRepository(_context);
            _chapterRepo = new ChapterRepository(_context);
        }

        [HttpPost]
        public IHttpActionResult Add(ChapterDto dto)
        {
            if (dto is null)
                return BadRequest();

            var userId = User.Identity.GetUserId();

            var manga = _mangaRepo.GetManga(dto.MangaId);

            if (manga == null)
                return NotFound();

            if (manga.ArtistId != userId)
                return Unauthorized();

            if (_chapterRepo.GetChapterForManga(dto.MangaId, dto.ChapterNo) != null)
                return BadRequest($"The Chapter {dto.ChapterNo} already exists for the specified Manga");

            var mangaChapter = new Chapter
            {
                MangaId = dto.MangaId,
                ChapterNo = dto.ChapterNo,
                NumberOfPages = dto.NumberOfPages,
                Information = dto.Information,
                DateTime = DateTime.Now
            };

            _chapterRepo.Add(mangaChapter);
            _context.SaveChanges();

            return Ok();
        }
    }
}