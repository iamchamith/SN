using Alpha.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alpha.Areas.UserAcccount.Controllers
{
    [OutputCache(Duration = int.MaxValue)]
    public class AuthenticationController : Controller
    {
        public ActionResult Index()
        {
            if (GCSession.IsSession)
            {
                return RedirectToAction("Index", "Settings");
            }
            return View();
        }
    }
}