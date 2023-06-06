using MangaHub.Core;
using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using MangaHub.Utility;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace MangaHub.Controllers.Api
{
    public class ReadingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReadingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         }

        [HttpPost, Authorize(Roles ="Reader")]
        public IHttpActionResult AddReadingForManga(ReadingDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
                return BadRequest(errorMessage);
            }

            if (dto is null)
                return BadRequest();

            var userId = User.Identity.GetUserId();         

            var manga = _unitOfWork.MangaRepo.GetManga(dto.MangaId);
            if (manga == null)
                return NotFound();

            if (_unitOfWork.ReadingRepo
                .GetReadingForManga(dto.MangaId, userId) != null)
                return Ok();

            var user = _unitOfWork.UserRepo.GetUser(userId);
            
            var reading = new Reading(user, manga);

            var notifier = new Notifier();
            var notificationMessage = $"{user.Name} is reading Manga: {manga.Title}";
            notifier.NotifyDelete(new[] {user}, notificationMessage);

            _unitOfWork.ReadingRepo.Add(reading);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete, Authorize(Roles = "Reader")]
        public IHttpActionResult RemoveReadingForManga(int id)
        {
            var reading = _unitOfWork.ReadingRepo
                .GetReadingForMangaWithUserAndManga(id, User.Identity.GetUserId());

            if (reading == null)
                return NotFound();

            var notifier = new Notifier();
            var notificationMessage = $"{reading.User.Name} has unread Manga: {reading.Manga.Title}";
            notifier.NotifyDelete(new[] { reading.User }, notificationMessage);

            _unitOfWork.ReadingRepo.Remove(reading);
            _unitOfWork.Complete();

            return Ok();

        }
    }
}