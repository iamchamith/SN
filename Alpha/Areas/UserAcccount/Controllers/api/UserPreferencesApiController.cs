using Alpha.Areas.UserAcccount.Models;
using Alpha.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Alpha.Utility;
using Alpha.Service.Interfaces;
using Alpha.Service.Services;
using Alpha.Service.Infrastructure;
using Alpha.Bo.Enums;
using Alpha.Bo.Bo;

namespace Alpha.Areas.UserAcccount.Controllers.api
{
    [RoutePrefix("api/v1/user"), Authorized, Compress]
    public class UserPreferencesApiController : BaseApiController, IApiService<UserPreferencesViewModel, int>
    {
        IUserPreferencesService servie;
        public UserPreferencesApiController()
        {
            servie = new UserPreferencesService(new UnitOfWork());
        }
        public Task<IHttpActionResult> Create(UserPreferencesViewModel item)
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route("preferences")]
        public async Task<IHttpActionResult> Read()
        {
            try
            {
                var response = await this.servie.Read(GCSession.UserGuid);
                return Ok<UserPreferencesViewModel>(new UserPreferencesViewModel
                {
                    SendNotificationEmail = response.FirstOrDefault(p => p.UserPreference == Enums.UserPreferencesInfo.SendNotificationEmail).State,
                    ShowAnonymas = response.FirstOrDefault(p => p.UserPreference == Enums.UserPreferencesInfo.ShowAnonymas).State,
                    ShowMyAnswers = response.FirstOrDefault(p => p.UserPreference == Enums.UserPreferencesInfo.ShowMyAnswers).State,
                    ShowMyAsk = response.FirstOrDefault(p => p.UserPreference == Enums.UserPreferencesInfo.ShowMyAsk).State,
                    ShowMyContacts = response.FirstOrDefault(p => p.UserPreference == Enums.UserPreferencesInfo.ShowMyContacts).State,
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public Task<IHttpActionResult> Read(int item)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Remove(int item)
        {
            throw new NotImplementedException();
        }
        [HttpPost, Route("preferences")]
        public async Task<IHttpActionResult> Update(UserPreferencesViewModel item)
        {
            try
            {
                var userid = GCSession.UserGuid;
                var userpreference = new List<UserPreferencesBo>();
                userpreference.AddRange(new List<UserPreferencesBo>() {
                    new UserPreferencesBo(){
                        UserPreference = Enums.UserPreferencesInfo.SendNotificationEmail,
                        State = item.SendNotificationEmail,
                        UserId = userid
                    },
                     new UserPreferencesBo(){
                          UserPreference = Enums.UserPreferencesInfo.ShowAnonymas,
                        State = item.ShowAnonymas,UserId = userid
                     },
                      new UserPreferencesBo(){
                           UserPreference = Enums.UserPreferencesInfo.ShowMyAnswers,
                        State = item.ShowMyAnswers,UserId = userid
                      },
                       new UserPreferencesBo(){
                            UserPreference = Enums.UserPreferencesInfo.ShowMyAsk,
                        State = item.ShowMyAsk,UserId = userid
                       },
                        new UserPreferencesBo(){
                             UserPreference = Enums.UserPreferencesInfo.ShowMyContacts,
                        State = item.ShowMyContacts,UserId=userid
                        }
                });

                await this.servie.Update(userpreference, GCSession.UserGuid);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
