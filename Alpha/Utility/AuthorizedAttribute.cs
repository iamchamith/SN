using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Alpha.Utility
{
    public class AuthorizedAttribute : AuthorizeAttribute
    {
        public string RoleTags { get; set; }
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //actionContext.Response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
            return GCSession.IsSession;
        }
    }
}