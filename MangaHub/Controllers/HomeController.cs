using MangaHub.Core;
using MangaHub.Core.ViewModels;
using MangaHub.Persistence;
using System.Web.Mvc;

namespace MangaHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
            
        }
        public ActionResult Index()
        {
            var viewModel = new MangaViewModel()
            {
                Mangas = _unitOfWork.MangaRepo.GetMangaWithChapters()
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