using Alpha.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alpha.Areas.UserAcccount.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: UserAcccount/Authontication
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