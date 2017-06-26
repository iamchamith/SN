using Alpha.Areas.UserAcccount.Models;
using Alpha.Bo;
using Alpha.Bo.Enums;
using Alpha.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alpha.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        [NonAction]
        public UserPreviewPageViewModel GetPreviewObject(UserBo item, bool ismine = true)
        {

            var result = new UserPreviewPageViewModel();
            result.Bio = item.Bio;
            result.Dob = item.Dob;
            result.Country = ((Enums.Countries)item.Country).ToString().Replace('_', ' ');
            result.Name = item.Name;
            result.MaritalStatus = ((Enums.MaritalStatus)item.MaritalStatus).ToString().Replace('_', ' ');
            result.Gender = ((Enums.Gender)item.Gender).ToString().Replace('_', ' ');
            result.ProfileImage = (ismine)?GCSession.ProfileImage:
                $"https://www.gravatar.com/avatar/{Alpha.Bo.Utility.Helper.MD5Hash(item.Email)}";
            return result;
        }
    }
}
