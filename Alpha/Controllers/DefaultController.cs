using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alpha.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return Redirect("/userAcccount/authentication");
        }
        [HttpGet]
        public ActionResult _404() {
            return View();
        }
        [HttpGet]
        public ActionResult _500() {
            return View();
        }
    }
}