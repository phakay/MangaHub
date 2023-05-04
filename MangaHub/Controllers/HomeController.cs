using MangaHub.Core.ViewModels;
using MangaHub.Persistence;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MangaHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var viewModel = new MangaViewModel()
            {
                Mangas = _context.Mangas
                .Include(m => m.Artist)
                .Include(m => m.Chapters)
                .ToList()
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