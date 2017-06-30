using Alpha.Areas.UserAcccount.Models;
using Alpha.Areas.UserAcccount.Models.criends;
using Alpha.Bo;
using Alpha.Bo.Bo;
using Alpha.Bo.Exceptions;
using Alpha.Controllers.Api;
using Alpha.Service.Infrastructure;
using Alpha.Service.Interfaces;
using Alpha.Service.Services;
using Alpha.Utility;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Alpha.Areas.UserAcccount.Controllers.api
{
    [RoutePrefix("api/v1/userprofile"), Authorized]
    public class UserProfileController : BaseApiController
    {
        IUserSettings service;
        IUserContact serviceContect;
        IUserTags tagService;
        IConnectCriends serviceConnectionCriends;
        public UserProfileController()
        {
            var uow = new UnitOfWork();
            service = new UserSettingService(uow);
            serviceContect = new UserContactService(uow);
            tagService = new UserTagsService(uow);
            serviceConnectionCriends = new ConnectCriendsService(uow);
        }
        [Route("preview"), HttpGet]
        public async Task<IHttpActionResult> Preview(string guid)
        {
            try
            {
                Guid userid = Guid.Empty;
                try
                {
                    userid = Guid.Parse(guid);
                }
                catch (Exception)
                {
                    throw new ObjectNotFoundException();
                }
                var basic = new UserBo();
                var contacts = new List<UserContactBo>();
                var tags = new List<UserTagBo>();
                basic = await this.service.Read(userid);
                contacts = await this.serviceContect.Read(userid);
                tags = await this.tagService.Read(userid);
                var relations = await this.serviceConnectionCriends.GetCriendsRelationCount(userid);
                var meAndThisRelation = await this.serviceConnectionCriends.GetCriendRelation(GCSession.UserGuid, userid);
                return Ok<CriendProfilePreviewViewModel>(new CriendProfilePreviewViewModel
                {
                    UserTags = tags.Select(x => AutoMapper.Mapper.Map<UserTagsViewModel>(x)).ToList(),
                    CriendsRelations = Mapper.Map<CriendsRelationsViewModel>(meAndThisRelation),
                    BasicInfo = GetPreviewObject(basic, relations, false),
                    UserContacts = contacts.Select(x => AutoMapper.Mapper.Map<UserContactsViewModel>(x)).ToList()
                });
            }
            catch (ObjectNotFoundException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
