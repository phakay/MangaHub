using MangaHub.Core;
using MangaHub.Core.Models;
using MangaHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace MangaHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public ViewResult Index()
        {
            var viewModel = new MangasViewModel
            {
                Mangas = _unitOfWork.MangaRepo.GetMangaWithChapters(),
                ShowInfo = User.Identity.IsAuthenticated,
                UserReadings = User.Identity.IsAuthenticated ? _unitOfWork.ReadingRepo
                                .GetReadingsForUser(User.Identity.GetUserId())
                                    .ToLookup(r => r.MangaId) : 
                                Enumerable.Empty<Reading>()
                                    .ToLookup(r => r.MangaId)

            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}