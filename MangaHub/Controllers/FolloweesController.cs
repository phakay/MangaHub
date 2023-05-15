using MangaHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace MangaHub.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var followees = _unitOfWork.UserRepo.GetFollowees(User.Identity.GetUserId());
            return View(followees);
        }
    }
}