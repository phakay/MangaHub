using AutoMapper;
using MangaHub.Core;
using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace MangaHub.Controllers.Api
{
    public class ChaptersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChaptersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         }

        [HttpPost, Authorize]
        public IHttpActionResult Add(ChapterDto dto)
        {
            
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
                return BadRequest();
            }

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

            var mangaChapter = Mapper.Map<ChapterDto, Chapter>(dto);

            _unitOfWork.ChapterRepo.Add(mangaChapter);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete, Authorize]
        public IHttpActionResult RemoveChapterForManga(string key)
        {
            var chapterKeyArray = key.Split(Chapter.KeyCodeDelimiter);
            
            
            if(chapterKeyArray.Length != 2 || 
                !int.TryParse(chapterKeyArray[0], out var mangaId) ||
                !int.TryParse(chapterKeyArray[1], out var chapterNo)
                )  
            {
                return BadRequest($"value for {nameof(key)} is not valid");
            }


            var userId = User.Identity.GetUserId();

            var manga = _unitOfWork.MangaRepo.GetManga(mangaId);

            if (manga == null)
                return NotFound();

            if (manga.ArtistId != userId)
                return Unauthorized();

            var chapter = _unitOfWork.ChapterRepo.GetChapterForManga(mangaId, chapterNo);

            if (chapter == null)
                return NotFound();

            _unitOfWork.ChapterRepo.Remove(chapter);

            _unitOfWork.Complete();

            return Ok();

        }
    }
}