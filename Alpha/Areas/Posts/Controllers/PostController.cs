using Alpha.Controllers;
using Alpha.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alpha.Areas.Posts.Controllers
{
    #if !DEBUG
      [OutputCache(Duration = int.MaxValue)]
    #endif
    public class PostController : BaseController
    {
        [HttpGet, Compress]
        public ActionResult Index()
        {
            var view = base.Rederector();
            return (view != null) ? view : View();
            return View();
        }

    }
}