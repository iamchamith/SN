using Alpha.Areas.Posts.Models;
using Alpha.Controllers.Api;
using Alpha.Service.Infrastructure;
using Alpha.Service.Interfaces;
using Alpha.Service.Services.post;
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
    public class PostReplyApiController : BaseApiController
    {
        IPostLikeService postLikeService;
        public PostReplyApiController()
        {
            var uow = new UnitOfWork();
            this.postLikeService = new PostLikeService(uow);
        }
        [Route("topost/likedislike"), HttpPost]
        public async Task<IHttpActionResult> LikeDislikePost(DoPostLIkeViewModel item)
        {
            try
            {
                await postLikeService.LikeDislikePost(GCSession.UserGuid, Guid.Parse(item.PostId), item.Type, item.IsSelect);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
