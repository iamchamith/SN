using Alpha.Areas.UserAcccount.Models;
using Alpha.Areas.UserAcccount.Models.criends;
using Alpha.Bo.Bo;
using Alpha.Bo.Enums;
using Alpha.Controllers.Api;
using Alpha.Models;
using Alpha.Service.Infrastructure;
using Alpha.Service.Interfaces;
using Alpha.Service.Services;
using Alpha.Service.Services.settings;
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
    [RoutePrefix("api/v1/criends"), Compress]
    public class ConnectCriendsController : BaseApiController
    {
        IConnectCriends service;
        public ConnectCriendsController()
        {
            this.service = new ConnectCriendsService(new UnitOfWork());
        }

        [Route("search/looksup"), HttpGet, Authorized, Compress]
        public async Task<IHttpActionResult> Lookups()
        {
            try
            {
                return Ok<LookupsViewModel>(new LookupsViewModel()
                {
                    Countries = LookupsService.Countries().Select(x => AutoMapper.Mapper.Map<DropdownViewModel>(x)).ToList(),
                    Genders = LookupsService.Gender().Select(x => AutoMapper.Mapper.Map<DropdownViewModel>(x)).ToList(),
                    Status = LookupsService.MaritalStatus().Select(x => AutoMapper.Mapper.Map<DropdownViewModel>(x)).ToList(),
                    Country = GCSession.Country,
                    Gender = GCSession.Sex,
                    MaritalStatus = GCSession.MaritalStatus

                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("search"), HttpPost, Authorized, Compress]
        public async Task<IHttpActionResult> Search(SearchCriendsRequestViewModel item)
        {
            try
            {
                item.OwnerId = GCSession.UserGuid;
                var r = await this.service.Search(Mapper.Map<SearchCriendsRequestBo>(item));
                var result = r.Select(x => AutoMapper.Mapper.Map<SearchCriendsResultViewModel>(x)).ToList();
                return Ok<List<SearchCriendsResultViewModel>>(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("relationship"), HttpPost, Authorized, Compress]
        public async Task<IHttpActionResult> CreateRelationship(CriendsRelationshipViewModel item)
        {
            try
            {
                await this.service.AddRemoveRelation(new CriendsRelationshipBo
                {
                    OparationType = item.OparationType ? Enums.YesNo.Yes : Enums.YesNo.No,
                    OwnerId = GCSession.UserGuid,
                    UserId = Guid.Parse(item.UserId),
                    State = (Enums.UserRelationshipStatus)item.State
                });
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        
    }
}
