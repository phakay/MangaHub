using MangaHub.Core;
using MangaHub.Core.Models;
using MangaHub.Core.ViewModels;
using MangaHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MangaHub.Controllers
{
    public class MangasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MangasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new MangaFormViewModel
            {
                Heading = "Add a Manga",
                Genres = new ApplicationDbContext().Genres.ToList()
            };
            return View("MangaForm",viewModel);
        }

        [HttpPost]
        [Authorize, ValidateAntiForgeryToken]
        public ActionResult Create(MangaFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepo.GetGenres();
                return View("MangaForm", viewModel);
            }

            var file = viewModel.PictureWrapper;
            var imageData = new byte[0];

            if (file != null && ModelState.IsValid)
            {
                bool isExtensionValid = (new[] { ".png", ".jpg", ".jpeg" })
                            .Contains(Path.GetExtension(file.FileName));

                if (!isExtensionValid)
                {
                    ModelState.AddModelError("Picture", "The file extension is invalid.");
                    viewModel.Genres = _unitOfWork.GenreRepo.GetGenres();
                    return View("MangaForm", viewModel);
                }

                using(var ms  = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    imageData = ms.ToArray();
                }
            }

            var manga = new Manga
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                ArtistId = User.Identity.GetUserId(),
                DateCreated = DateTime.Now,
                GenreId = viewModel.Genre,
                Picture = imageData
            };

            _unitOfWork.MangaRepo.Add(manga);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Mangas");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var mangas = _unitOfWork.MangaRepo
                .GetMangaWithChapters(User.Identity.GetUserId());

            return View(mangas);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var manga = _unitOfWork.MangaRepo.GetManga(id);

            if (manga == null)
                return HttpNotFound();

            if (manga.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new MangaFormViewModel
            {
                Id = manga.Id,
                Heading = "Edit a Manga",
                Title = manga.Title,
                Description = manga.Description,
                Genre = manga.GenreId,
                Genres = _unitOfWork.GenreRepo.GetGenres(),
                Picture = manga.Picture,
                Chapters = _unitOfWork.ChapterRepo.GetChaptersForManga(manga.Id)
            };

            return View("MangaForm",viewModel);
        }

        [HttpPost]
        [Authorize, ValidateAntiForgeryToken]
        public ActionResult Update(MangaFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepo.GetGenres();
                return View("MangaForm", viewModel);
            }

            var file = viewModel.PictureWrapper;
            var imageData = viewModel.Picture;

            if (file != null && file.ContentLength > 0 && ModelState.IsValid)
            {
                bool isExtensionValid = (new[] { ".png", ".jpg", ".jpeg" })
                            .Contains(Path.GetExtension(file.FileName));

                if (!isExtensionValid)
                {
                    ModelState.AddModelError("Picture", "The file extension is invalid.");
                    viewModel.Genres = _unitOfWork.GenreRepo.GetGenres();
                    return View("MangaForm", viewModel);
                }

                using (var ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    imageData = ms.ToArray();
                }
            }


            var manga = _unitOfWork.MangaRepo.GetManga(viewModel.Id);

            if (manga == null)
                return HttpNotFound();

            manga.Update(new Manga
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                ArtistId = User.Identity.GetUserId(),
                DateCreated = DateTime.Now,
                GenreId = viewModel.Genre,
                Picture = imageData
            });

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Mangas");
        }
    }
}