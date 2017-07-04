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
using AutoMapper;
using Alpha.Bo;
using Alpha.Bo.Bo.posts;

namespace Alpha.Areas.Posts.Controllers.api
{
    [RoutePrefix("api/v1"), Compress, Authorized]
    public class PostReplyApiController : BaseApiController
    {
        IPostLikeService postLikeService;
        IPostCommentService postcommentService;
        public PostReplyApiController()
        {
            var uow = new UnitOfWork();
            this.postLikeService = new PostLikeService(uow);
            this.postcommentService = new PostCommentService(uow);
        }
        #region post like and dislike
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
        #endregion

        #region post comment
        [Route("topost/comment"), HttpPost]
        public async Task<IHttpActionResult> PostCommentInsert(PostCommentViewModel item)
        {
            try
            {
                item.CommentDate = DateTime.UtcNow;
                item.UserId = GCSession.UserGuid;
                var r = await this.postcommentService.Insert(Mapper.Map<PostCommentBo>(item));
                return Ok<PostCommentViewModel>(Mapper.Map<PostCommentViewModel>(r));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("topost/comment/remove"), HttpPost]
        public async Task<IHttpActionResult> PostCommentRemove(PostCommentViewModel item)
        {
            try
            {
                await this.postcommentService.Delete(item.PostId, GCSession.UserGuid);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("topost/comment"), HttpGet]
        public async Task<IHttpActionResult> ReadPostComment(string postid)
        {
            try
            {
                var r = await this.postcommentService.Search(Guid.Parse(postid));
                return Ok<List<PostCommentSearchResponse>>(r);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        #endregion
    }
}
