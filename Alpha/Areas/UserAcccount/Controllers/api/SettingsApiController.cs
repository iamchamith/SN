using Alpha.Areas.UserAcccount.Models;
using Alpha.Bo;
using Alpha.Bo.Bo;
using Alpha.Bo.Enums;
using Alpha.Bo.Exceptions;
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
    [RoutePrefix("api/v1/user/settings"), Compress]
    public class SettingsApiController : BaseApiController
    {
        IUserSettings service;
        IAuthontication serviceAuth;
        IUserContact serviceContect;
        public SettingsApiController()
        {
            var uow = new UnitOfWork();
            service = new UserSettingService(uow);
            serviceAuth = new AuthonticationService(uow);
            serviceContect = new UserContactService(uow);
        }
        #region basic info
        [Route("looksup"), HttpGet, Authorized]
        public async Task<IHttpActionResult> BasicInfoLooksup()
        {
            try
            {
                return Ok<LookupsViewModel>(new LookupsViewModel()
                {
                    Countries = LookupsService.Countries().Select(x => AutoMapper.Mapper.Map<DropdownViewModel>(x)).ToList(),
                    Genders = LookupsService.Gender().Select(x => AutoMapper.Mapper.Map<DropdownViewModel>(x)).ToList(),
                    Status = LookupsService.MaritalStatus().Select(x => AutoMapper.Mapper.Map<DropdownViewModel>(x)).ToList()
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("basic"), HttpPost, Authorized, ValidateModel]
        public async Task<IHttpActionResult> BasicInfoSave(UserBasicViewModel item)
        {
            try
            {
                item.UserId = GCSession.UserGuid;
                if (item.Email.Trim().ToLower() == GCSession.Email)
                {
                    item.IsValiedEmail = false;
                }
                await this.service.Update(Mapper.Map<UserBo>(item));
                var session = GCSession.User;
                session.Country = item.Country;
                session.Name = item.Name;
                session.Sex = item.Gender;
                session.MaritalStatus = item.MaritalStatus;
                GCSession.User = session;
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("basic"), HttpGet, Authorized, ValidateModel]
        public async Task<IHttpActionResult> UserBasicInfo()
        {
            try
            {
                var result = await this.service.Read(GCSession.UserGuid);
                return Ok<UserBasicViewModel>(Mapper.Map<UserBasicViewModel>(result));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpPost, Route("requestvalidateemailtoken")]
        public async Task<IHttpActionResult> SendEmailValidationToken()
        {

            try
            {
                var token = await this.service.SendValidateEmailToken(GCSession.UserGuid);
                var taskList = new List<Task>();
                Email.Send(new Alpha.Models.EmailModel
                {
                    Body = token,
                    Subject = "Validate email token",
                    ToPrimary = GCSession.Email
                });
                return Ok();
            }
            catch (EmailSendFailException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpGet, Route("validateemailvalidationtoken"), Authorized]
        public async Task<IHttpActionResult> ValidateEmailValidationToken(string token = "00000")
        {
            try
            {
                await this.service.ValidateEmailToken(token, GCSession.UserGuid);
                return Ok();
            }
            catch (InvaliedTokenException ex)
            {
                return BadRequest("invalied token.please try again");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        #endregion
        #region Preview page
        [HttpGet, Route("priviewpage"), Authorized]
        public async Task<IHttpActionResult> PriviewPage()
        {
            try
            {
                var result = await this.service.Read(GCSession.UserGuid);
                return Ok<UserPreviewPageViewModel>(GetPreviewObject(result));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
      
        #endregion

        [HttpPost, Route("changepassword"), Authorized, ValidateModel]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordViewModel item)
        {
            try
            {
                await this.serviceAuth.ChangePassword(new ChangePasswordBo
                {
                    UserId = GCSession.UserGuid,
                    Password = item.CurrentPassword,
                    NewPassword = item.NewPassword,
                    Email = GCSession.Email
                });
                return Ok();
            }
            catch (InvaliedUserInputsException e)
            {
                return BadRequest(" invalied current password");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        #region update tags
        public async Task<IHttpActionResult> FilterTags() { return null; }

        public async Task<IHttpActionResult> TagsSave() { return null; }
        #endregion

        #region contacts
        [HttpGet, Authorized, Route("usercontactslooksup")]
        public async Task<IHttpActionResult> ContactsLookups()
        {
            try
            {
                var result = this.serviceContect.Read(true)
                    .Select(x => AutoMapper.Mapper.Map<DropdownViewModel>(x)).ToList();
                result.Insert(0, new DropdownViewModel { Text = "Select Contact", Value = "-1" });
                return Ok<List<DropdownViewModel>>(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpGet, Authorized, Route("contactdetails/{id:int}")]
        public async Task<IHttpActionResult> GetContactDetail(int id)
        {
            try
            {
                var r = await this.serviceContect.Read((Enums.SocialNetworks)id, GCSession.UserGuid);
                return Ok<UserContactsViewModel>(Mapper.Map<UserContactsViewModel>(r) ?? new UserContactsViewModel());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpGet, Authorized, Route("contactdetailssummery")]
        public async Task<IHttpActionResult> GetContactDetail()
        {
            try
            {
                var r = await this.serviceContect.Read(GCSession.UserGuid);
                return Ok<List<UserContactsViewModel>>(r.Select(x => AutoMapper.Mapper.Map<UserContactsViewModel>(x)).ToList());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Authorized, Route("contactdetails"), ValidateModel]
        public async Task<IHttpActionResult> UpdateContactDetails(UserContactsViewModel item)
        {
            try
            {
                item.UserId = GCSession.UserGuid;
                await this.serviceContect.Update(Mapper.Map<UserContactBo>(item));
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpPost, Authorized, Route("contactdetails/{id:int}")]
        public async Task<IHttpActionResult> DeleteContactDetail(int id)
        {
            try
            {
                await this.serviceContect.Delete((Enums.SocialNetworks)id, GCSession.UserGuid);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        #endregion
    }
}
