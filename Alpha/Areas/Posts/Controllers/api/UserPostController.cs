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

namespace Alpha.Areas.Posts.Controllers
{
    [RoutePrefix("api/v1")]
    public class UserPostController : BaseApiController, IApiService<PostViewModel, string>
    {
        IUserPost service;
        public UserPostController()
        {
            service = new UserPostService(new UnitOfWork());
        }
        [Route("post"), HttpPost, Authorized, ValidateModel]
        public async Task<IHttpActionResult> Create(PostViewModel item)
        {
            try
            {
                var r = await this.service.Insert(new Bo.UserPostInfoBo
                {
                    Post = Mapper.Map<PostBo>(item),
                    UserPost = new UserPostBo
                    {
                        Anonymous = item.IsAnonymas ? Bo.Enums.Enums.YesNo.Yes : Bo.Enums.Enums.YesNo.No,
                        UserId = GCSession.UserGuid
                    }
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

        [Route("post/delete"), HttpPost, Authorized]
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
    }
}
