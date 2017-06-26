using Alpha.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alpha.Areas.Posts.Controllers
{
    public class PostController : Controller
    {
        [HttpGet, Compress]
        public ActionResult Index()
        {
            if (!GCSession.IsSession)
            {
                return RedirectToAction("Index", "../UserAcccount/Authentication");
            }
            return View();
        }
        
    }
}