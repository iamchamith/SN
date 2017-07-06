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
                await postLikeService.LikeDislikePost(GCSession.UserGuid, Guid.Parse(item.PostId), item.Type, item.PostLikeModeType, item.IsSelect);
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
                item.PostId = Guid.Parse(item.PostIdStr);
                item.UserId = GCSession.UserGuid;
                var r = await this.postcommentService.Insert(Mapper.Map<PostCommentBo>(item));
                var response = new PostCommentSearchResponse
                {
                    Comment = r.Comment,
                    CommentId = r.CommentId,
                    ProfileImage = GCSession.ProfileImage,
                    CommentDateStr = "just now",
                    Name = GCSession.UserDisplayName,
                    UserId = GCSession.UserGuid
                };
                return Ok<PostCommentSearchResponse>(response);
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
                await this.postcommentService.Delete(item.CommentId, GCSession.UserGuid);
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
                var r = await this.postcommentService.Search(Guid.Parse(postid), GCSession.UserGuid);
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
