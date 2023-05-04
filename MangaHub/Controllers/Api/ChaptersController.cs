﻿using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using MangaHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace MangaHub.Controllers.Api
{
    public class ChaptersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ChaptersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Post(ChapterDto dto)
        {
            if (dto is null)
                return BadRequest();

            var userId = User.Identity.GetUserId();

            var manga = _context.Mangas
                .SingleOrDefault(m => m.Id == dto.MangaId);

            if (manga == null)
                return NotFound();

            if (manga.ArtistId != userId)
                return Unauthorized();

            if (_context.Chapters
                .Any(c => c.MangaId == dto.MangaId &&
                c.ChapterNo == dto.ChapterNo))
                return BadRequest($"Chapter {dto.ChapterNo} already exists for the specified Manga: {manga.Title}");

            var mangaChapter = new Chapter
            {
                MangaId = dto.MangaId,
                ChapterNo = dto.ChapterNo,
                NumberOfPages = dto.NumberOfPages,
                Information = dto.Information,
                DateTime = DateTime.Now
            };

            _context.Chapters.Add(mangaChapter);
            _context.SaveChanges();

            return Ok();
        }
    }
}