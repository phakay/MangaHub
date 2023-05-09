using MangaHub.Core;
using Microsoft.AspNet.Identity;
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

            var manga = _unitOfWork.MangaRepo.GetManga(id);

            if (manga == null)
                return NotFound();

            if (manga.ArtistId != userId)
                return Unauthorized();

            _unitOfWork.MangaRepo.Remove(manga);

            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}