using Alpha.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alpha.Controllers
{
    public class BaseController : Controller
    {

        public ActionResult Rederector()
        {
            if (!GCSession.IsSession)
            {
                return RedirectToAction("Index", "Authentication");
            }
            else if (GCSession.IsStater)
            {
                return RedirectToAction("Stater", "Settings");
            }
            return null;
        }
    }
}