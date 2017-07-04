using Alpha.Controllers.Api;
using Alpha.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Areas.Posts.Controllers.api
{
    [RoutePrefix("api/v1"), Compress, Authorized]
    public class PostLikeApiController : BaseApiController
    {
        [Route("likedislike"), HttpPost]
        public async Task<IHttpActionResult> LikeDislikePost(string postId, PostLikeType type, bool clicked)
        {
            return null;
        }
    }
}
