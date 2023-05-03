using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaHub.Controllers
{
    public class ChaptersController : Controller
    {
        // GET: Chapters
        public ActionResult Index()
        {
            return View();
        }
    }
}