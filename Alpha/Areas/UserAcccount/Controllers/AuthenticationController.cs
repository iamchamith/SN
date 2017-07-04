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
    public class AuthenticationController : BaseController
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