using Alpha.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alpha.Areas.UserAcccount.Controllers
{
    //[OutputCache(Duration = int.MaxValue)]
    public class SettingsController : Controller
    {
        [HttpGet, Compress]
        public ActionResult Index()
        {
            if (!GCSession.IsSession)
            {
                return RedirectToAction("index", "authentication");
            }
            return View();
        }
        [HttpGet, Compress]
        public ActionResult Search()
        {
            if (!GCSession.IsSession)
            {
                return RedirectToAction("index", "authentication");
            }
            return View();
        }

        [HttpGet, Compress]
        public ActionResult Stater()
        {
            if (!GCSession.IsSession)
            {
                return RedirectToAction("index", "authentication");
            }
            return View();
        }
    }
}