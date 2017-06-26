using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Alpha.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new Bundle("~/bundles/css").Include(
              "~/Content/site.bootstrap.css",
              "~/Content/style.css",
              "~/Content/kendo/kendo.rtl.min.css",
             "~/Content/kendo/kendo.silver.min.css",
             "~/Content/kendo/kendo.mobile.all.min.css",
              "~/Content/font-awesome.min.css",
              "~/Content/animate.min.css",
              "~/Content/Site2.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
