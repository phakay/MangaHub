using MangaHub.Core;
using MangaHub.Core.Models;
using MangaHub.Core.ViewModels;
using MangaHub.Persistence;
using Microsoft.AspNet.Identity;
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

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult Create(MangaFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepo.GetGenres();
                return View("MangaForm", viewModel);
            }

            var file = viewModel.PictureWrapper;
            viewModel.Picture = new byte[0];

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
                    viewModel.Picture = ms.ToArray();
                }
            }

            var artist = _unitOfWork.UserRepo.GetUserWithFollowers(User.Identity.GetUserId());
            var manga = new Manga
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Artist = artist,
                GenreId = viewModel.Genre,
                Picture = viewModel.Picture
            };
            manga.NotifyCreate();

            _unitOfWork.MangaRepo.Add(manga);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Mangas");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var mangas = User.IsInRole("Reader") ? 
                _unitOfWork.MangaRepo.GetMangasReadByUser(userId) :
                 _unitOfWork.MangaRepo.GetMangasWithChapters(userId);
            
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

            var manga = _unitOfWork.MangaRepo.GetMangaWithReaders(viewModel.Id);
            var genre = _unitOfWork.GenreRepo.Get(viewModel.Genre);

            if(genre == null)
                return HttpNotFound("Manga was not found");
            if (genre == null)
                return HttpNotFound("Genre was not found");
            
            manga.UpdateAndNotify(new Manga
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                GenreId = viewModel.Genre,
                Genre = genre,
                Picture = imageData
            });

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Mangas");
        }

        public ActionResult Details(int id)
        {
            var manga = _unitOfWork.MangaRepo.GetManga(id);

            if (manga == null)
                return HttpNotFound();

            var viewModel = new MangaDetailsViewModel
            {
                Manga = manga
            };

            if (User.Identity.IsAuthenticated)
            {
                viewModel.IsReading = _unitOfWork.ReadingRepo
                    .GetReadingForManga(manga.Id, User.Identity.GetUserId()) != null;

                viewModel.IsFollowing = _unitOfWork.FollowingRepo
                    .GetFollowing(User.Identity.GetUserId(), manga.ArtistId) != null;
            }

            return View(viewModel);
        }
    }
}