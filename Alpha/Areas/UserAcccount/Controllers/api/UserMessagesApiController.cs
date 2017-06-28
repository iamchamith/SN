using Alpha.Bo.Bo.settings;
using Alpha.Controllers.Api;
using Alpha.Service.Infrastructure;
using Alpha.Service.Interfaces;
using Alpha.Service.Services.settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Alpha.Areas.UserAcccount.Models;
using Alpha.Utility;

namespace Alpha.Areas.UserAcccount.Controllers.api
{
    [RoutePrefix("api/v1/user"), Compress, Authorized]
    public class UserMessagesApiController : BaseApiController
    {
        IUserMessageService service;
        public UserMessagesApiController()
        {
            service = new UserMessageService(new UnitOfWork());
        }
        [HttpGet, Route("message")]
        public IHttpActionResult Read(UserMessageSendRequestBo item)
        {
            try
            {
                item.FromUser = GCSession.UserGuid;
                var response = this.service.Read(item)
                     .Select(x => AutoMapper.Mapper.Map<UserMessageViewModel>(x)).ToList();
                return Ok<List<UserMessageViewModel>>(response);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpPost, Route("message")]
        public async Task<IHttpActionResult> Send(UserMessageViewModel item)
        {
            try
            {
                item.FromUser = GCSession.UserGuid;
                var r = await this.service.Send(Mapper.Map<UserMessageBo>(item));
                return Ok<UserMessageViewModel>(Mapper.Map<UserMessageViewModel>(r));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
