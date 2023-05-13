using AutoMapper;
using MangaHub.Core;
using MangaHub.Core.Models;
using MangaHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace MangaHub.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChaptersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Create(int mangaId)
        {
            var manga = _unitOfWork.MangaRepo.GetManga(mangaId);
            if (manga == null)
                return HttpNotFound();

            var viewModel = new ChapterFormViewModel
            {
                MangaId = manga.Id,
                MangaPicture = manga.Picture,
                MangaTitle = manga.Title
            };

            return View("ChapterForm",viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(ChapterFormViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("ChapterForm", viewModel);
            }

            var manga = _unitOfWork.MangaRepo.GetManga(viewModel.MangaId);
            if (manga == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            if (manga.ArtistId != userId)
                return new HttpUnauthorizedResult();

            if (_unitOfWork.ChapterRepo.GetChapterForManga(viewModel.MangaId, viewModel.ChapterNo) != null)
            {
                ModelState.AddModelError("ChapterNo",
                    $"The Chapter {viewModel.ChapterNo} already exists for the specified Manga");
                return View("ChapterForm", viewModel);
            }

            var mangaChapter = Mapper.Map<ChapterFormViewModel, Chapter>(viewModel);
            _unitOfWork.ChapterRepo.Add(mangaChapter);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Mangas");
        }
    }
}