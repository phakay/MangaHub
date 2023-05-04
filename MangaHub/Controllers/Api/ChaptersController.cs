using MangaHub.Core;
using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using MangaHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace MangaHub.Controllers.Api
{
    public class ChaptersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChaptersController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        [HttpPost]
        public IHttpActionResult Add(ChapterDto dto)
        {
            if (dto is null)
                return BadRequest();

            var userId = User.Identity.GetUserId();

            var manga = _unitOfWork.MangaRepo.GetManga(dto.MangaId);

            if (manga == null)
                return NotFound();

            if (manga.ArtistId != userId)
                return Unauthorized();

            if (_unitOfWork.ChapterRepo.GetChapterForManga(dto.MangaId, dto.ChapterNo) != null)
                return BadRequest($"The Chapter {dto.ChapterNo} already exists for the specified Manga");

            var mangaChapter = new Chapter
            {
                MangaId = dto.MangaId,
                ChapterNo = dto.ChapterNo,
                NumberOfPages = dto.NumberOfPages,
                Information = dto.Information,
                DateTime = DateTime.Now
            };
            _unitOfWork.ChapterRepo.Add(mangaChapter);

            _unitOfWork.Complete();

            return Ok();
        }
    }
}