using Alpha.App_Start;
using Alpha.Utility;
using System;
using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Alpha.DbAccess;
namespace Alpha
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //var configuration = new Alpha.DbAccess.Migrations.Configuration();
            //var migrator = new DbMigrator(configuration);
            //migrator.Update();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutomapperConfig.Register();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
        
        }
    }
}
