using MangaHub.Core.ViewModels;
using MangaHub.Persistence;
using MangaHub.Persistence.Repositories;
using System.Web.Mvc;

namespace MangaHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MangaRepository _mangaRepo;


        public HomeController()
        {
            _context = new ApplicationDbContext();
            _mangaRepo = new MangaRepository(_context);
        }
        public ActionResult Index()
        {
            var viewModel = new MangaViewModel()
            {
                Mangas = _mangaRepo.GetMangaWithChapters()
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