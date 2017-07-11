using Alpha.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Alpha.Areas.Posts.Models;
using Alpha.Utility;
using Alpha.Service.Interfaces;
using Alpha.Service.Services;
using Alpha.Service.Infrastructure;
using AutoMapper;
using Alpha.Bo;
using Alpha.Bo.Bo;
using Alpha.Bo.Bo.posts;
using Alpha.Bo.Enums;
using reCAPTCHA.MVC;

namespace Alpha.Areas.Posts.Controllers
{
    [RoutePrefix("api/v1")]
    public class PostApiController : BaseApiController, IApiService<PostViewModel, string>
    {
        IUserPost service;
        public PostApiController()
        {
            service = new UserPostService(new UnitOfWork());
        }
        [Route("post/question"), HttpPost, Authorized, ValidateModel, CaptchaValidator]
        public async Task<IHttpActionResult> Create(UserPostQuestionViewModel item)
        {
            try
            {
                var r = await this.service.Insert(Mapper.Map<PostQuestionBo>(item)
                    , new UserPostBo
                    {
                        Anonymous = item.IsAnonymas ? Bo.Enums.Enums.YesNo.Yes : Bo.Enums.Enums.YesNo.No,
                        UserId = GCSession.UserGuid,
                        ResponseMinits = item.Days * 1440
                    });
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("post/poll"), HttpPost, Authorized, ValidateModel, CaptchaValidator]
        public async Task<IHttpActionResult> Create(UserPostPollViewModel item)
        {
            try
            {
                item.Vs1Url = base.UploadImage(item.Vs1Data, Enums.Imagetype.postimages, Guid.NewGuid().ToString());
                item.Vs2Url = base.UploadImage(item.Vs2Data, Enums.Imagetype.postimages, Guid.NewGuid().ToString());
                var r = await this.service.Insert(Mapper.Map<PostPollBo>(item)
                    , new UserPostBo
                    {
                        Anonymous = item.IsAnonymas ? Bo.Enums.Enums.YesNo.Yes : Bo.Enums.Enums.YesNo.No,
                        UserId = GCSession.UserGuid
                    });
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("post/comment"), HttpPost, Authorized, ValidateModel, CaptchaValidator]
        public async Task<IHttpActionResult> Create(UserPostNeedCommentViewModel item)
        {
            try
            {
                item.ImageUrl = base.UploadImage(item.AskCommentImage, Enums.Imagetype.postimages, Guid.NewGuid().ToString());
                var r = await this.service.Insert(Mapper.Map<PostNeedCommentBo>(item)
                     , new UserPostBo
                     {
                         Anonymous = item.IsAnonymas ? Bo.Enums.Enums.YesNo.Yes : Bo.Enums.Enums.YesNo.No,
                         UserId = GCSession.UserGuid
                     });
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        public async Task<IHttpActionResult> Read()
        {
            throw new NotImplementedException();
        }
        [Route("post/search"), HttpPost, Authorized]
        public async Task<IHttpActionResult> Search(UserPostSearchRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserId))
                {
                    request.UserId = GCSession.UserGuid.ToString();
                }
                request.MyUserId = GCSession.UserGuid;
                return Ok<List<UserPostSearchResponse>>(await this.service.SearchPost(request));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("post/{item}"), HttpGet, Authorized]
        public async Task<IHttpActionResult> Read(string item)
        {
            try
            {
                var r = await this.service.Read(Guid.Parse(item), GCSession.UserGuid);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("post/delete"), HttpGet, Authorized]
        public async Task<IHttpActionResult> Remove(string item)
        {
            try
            {
                await this.service.Delete(Guid.Parse(item), GCSession.UserGuid);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public Task<IHttpActionResult> Update(PostViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Create(PostViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
