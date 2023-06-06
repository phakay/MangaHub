using MangaHub.Core;
using MangaHub.Utility;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace MangaHub.Controllers.Api
{
    public class MangasController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public MangasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete, Authorize]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();

            var manga = _unitOfWork.MangaRepo.GetMangaWithReaders(id);

            if (manga == null)
                return NotFound();

            if (manga.ArtistId != userId)
                return Unauthorized();

            var notifier = new Notifier();
            var notificationMessage = $"{manga.Artist.Name} removed Manga: {manga.Title}";
            notifier.NotifyDelete(manga.Readings.Select(r => r.User), notificationMessage);

            _unitOfWork.MangaRepo.Remove(manga);

            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}