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
                Genres = new ApplicationDbContext().Genres.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize, ValidateAntiForgeryToken]
        public ActionResult Create(MangaFormViewModel viewModel)
        {
            var file = viewModel.Picture;
            var imageData = new byte[0];

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepo.GetGenres();
                return View("Create", viewModel);
            }

            if (file != null && ModelState.IsValid)
            {
                bool isExtensionValid = (new[] { ".png", ".jpg", ".jpeg" })
                            .Contains(Path.GetExtension(file.FileName));

                if (!isExtensionValid)
                {
                    ModelState.AddModelError("Picture", "The file extension is invalid.");
                    viewModel.Genres = _unitOfWork.GenreRepo.GetGenres();
                    return View("Create", viewModel);
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

            return RedirectToAction("Index", "Home");
        }


    }
}