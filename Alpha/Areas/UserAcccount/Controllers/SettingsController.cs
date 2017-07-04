using Alpha.Controllers;
using Alpha.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alpha.Areas.UserAcccount.Controllers
{
#if !DEBUG
      [OutputCache(Duration = int.MaxValue)]
#endif
    public class SettingsController : BaseController
    {
        [HttpGet, Compress]
        public ActionResult Index()
        {
            var view = base.Rederector();
            return (view != null) ? view : View();
        }
        [HttpGet, Compress]
        public ActionResult Search()
        {
            var view = base.Rederector();
            return (view != null) ? view : View();
        }

        [HttpGet, Compress]
        public ActionResult Stater()
        {
            if (!GCSession.IsSession)
            {
                return RedirectToAction("Index", "Authentication");
            }
            return View();
        }
    }
}