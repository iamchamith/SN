using Alpha.Areas.UserAcccount.Models;
using Alpha.Service.Infrastructure;
using Alpha.Service.Interfaces;
using Alpha.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Alpha.Controllers.Api;
using Alpha.Models;
using AutoMapper;
using Alpha.Bo;
using Alpha.Utility;
using Alpha.Bo.Exceptions;

namespace Alpha.Areas.UserAcccount.Controllers.api
{
    [RoutePrefix("api/v1"), Compress]
    public class TagsApiController : BaseApiController, IApiService<TagViewModel, int>
    {
        ITags service;
        IUserTags serviceUserTags;
        public TagsApiController()
        {
            var uow = new UnitOfWork();
            service = new TagService(uow);
            serviceUserTags = new UserTagsService(uow);
        }
        [HttpPost, Route("tag/create"), Authorized, ValidateModel]
        public async Task<IHttpActionResult> Create(TagViewModel item)
        {
            try
            {
                item.Owner = GCSession.UserGuid;
                var result = await this.service.Insert(Mapper.Map<TagBo>(item));
                await this.serviceUserTags.Insert(new UserTagBo
                {
                    TagId = result.Id,
                    UserId = GCSession.UserGuid
                });
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpGet, Route("tag/search"), Authorized]
        public async Task<IHttpActionResult> Read(string q = "a")
        {
            try
            {
                var result = await this.service.Read(q, GCSession.UserGuid);
                return Ok<List<DropdownViewModel>>(result.Select(x => AutoMapper.Mapper.Map<DropdownViewModel>(x)).ToList());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpPost, Route("tag/add/{tagId:int}"), Authorized, ValidateModel]
        public async Task<IHttpActionResult> Add(int tagId)
        {
            try
            {
                await this.serviceUserTags.Insert(new UserTagBo
                {
                    TagId = tagId,
                    UserId = GCSession.UserGuid
                });
                return Ok();
            }
            catch (PrimaryKeyViolationException e)
            {
                return BadRequest("already exist");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost, Route("tag/remove/{item:int}"), Authorized, ValidateModel]
        public async Task<IHttpActionResult> Remove(int item)
        {
            try
            {
                await this.serviceUserTags.Delete(item, GCSession.UserGuid);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public Task<IHttpActionResult> Update(TagViewModel item)
        {
            throw new NotImplementedException();
        }
        [HttpGet, Route("tag/read"), Authorized]
        public async Task<IHttpActionResult> Read()
        {
            try
            {
                var r = await this.serviceUserTags.Read(GCSession.UserGuid);
                return Ok<List<UserTagsViewModel>>(r.Select(x => AutoMapper.Mapper.Map<UserTagsViewModel>(x)).ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("tag/read/{item:int}"), Authorized]
        public async Task<IHttpActionResult> Read(int item)
        {
            try
            {
                var r = await this.serviceUserTags.Read(GCSession.UserGuid, item);
                return Ok<TagInfoViewModel>(Mapper.Map<TagInfoViewModel>(r));
            }
            catch (ObjectNotFoundException e)
            {
                return BadRequest("tag not found");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
