using MangaHub.Models;
using MangaHub.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MangaHub.Controllers
{
    public class MangasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MangasController()
        {
            _context = new ApplicationDbContext();
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
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }

            if (file != null && ModelState.IsValid)
            {
                bool isExtensionValid = (new[] { ".png", ".jpg", ".jpeg" })
                            .Contains(Path.GetExtension(file.FileName));

                if (!isExtensionValid)
                {
                    ModelState.AddModelError("Picture", "The file extension is invalid.");
                    viewModel.Genres = _context.Genres.ToList();
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

            _context.Mangas.Add(manga);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}