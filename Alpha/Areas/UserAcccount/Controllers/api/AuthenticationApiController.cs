using Alpha.Areas.UserAcccount.Models;
using Alpha.Bo;
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

namespace Alpha.Areas.UserAcccount.Controllers
{
    [RoutePrefix("api/v1/auth"), AllowAnonymous, Compress]
    public class AuthenticationApiController : BaseApiController
    {
        IAuthontication service;
        public AuthenticationApiController()
        {
            service = new AuthonticationService(new UnitOfWork());
        }
        [Route("register"), HttpPost, ValidateModel, Compress]
        public async Task<IHttpActionResult> Register(RegisterViewModel item)
        {
            try
            {
                var r = await this.service.Register(Mapper.Map<UserBo>(item));
                GCSession.User = Mapper.Map<SessionUser>(r);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("login"), HttpPost, ValidateModel, Compress]
        public async Task<IHttpActionResult> Login(LoginViewModel item)
        {
            try
            {
                GCSession.User = Mapper.Map<SessionUser>(this.service.Login(Mapper.Map<UserLoginBo>(item)));
                return Ok();
            }
            catch (InvaliedUserInputsException e)
            {
                return BadRequest(" login failed.invalied user name or password");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("logout"), HttpPost, ValidateModel, Compress]
        public async Task<IHttpActionResult> Logout()
        {
            try
            {
                GCSession.Logout();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet, AllowAnonymous, Route("forgetpasswordrequest"), Compress]
        public async Task<IHttpActionResult> ForgetPasswordRequest(string email)
        {
            try
            {
                var result = await this.service.ForgetPasswordRequest(email);
                Email.Send(new Alpha.Models.EmailModel
                {
                    UserName = result[1],
                    Body = $"Please use <b>{result[0]} </b> code for validate yourself :)",
                    Subject = "Forget password token",
                    ToPrimary = email.Trim().ToLower()
                });
                return Ok();
            }
            catch (EmailSendFailException e)
            {
                return BadRequest(e.Message);
            }
            catch (ObjectNotFoundException e)
            {
                return BadRequest("email not founed");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, AllowAnonymous, Route("forgetpasswordrequesttokenvalidate"), Compress]
        public async Task<IHttpActionResult> ForgetPasswordRequestTokenValidate(string email, string token)
        {
            try
            {
                var result = await service.ForgetPasswordRequestTokenValidate(token, email);
                GCSession.User = Mapper.Map<SessionUser>(result);
                return Ok();
            }
            catch (InvaliedTokenException e)
            {
                return BadRequest("invalied token");
            }
            catch (ObjectNotFoundException e)
            {
                return BadRequest("email not founed");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost, Authorized, Route("changepassword"), Compress]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordViewModel item)
        {
            try
            {
                await this.service.ChangePassword(item.NewPassword, GCSession.UserGuid);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
