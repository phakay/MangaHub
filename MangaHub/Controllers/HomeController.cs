using MangaHub.Core;
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
        public ViewResult Index(string id = null)
        {
            var viewModel = new MangasViewModel
            {
                Mangas = _unitOfWork.MangaRepo.GetMangasWithChapters(id)
            };

            if (User.Identity.IsAuthenticated && User.IsInRole("Reader"))
            {
                viewModel.ShowInfo = true;
                viewModel.UserReadings = _unitOfWork.ReadingRepo
                                .GetReadingsForUser(User.Identity.GetUserId())
                                    .ToLookup(r => r.MangaId);
            }
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