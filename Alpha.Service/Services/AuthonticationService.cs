using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Bo;
using Alpha.Service.Infrastructure;
using Alpha.Poco;
using AutoMapper;
using Alpha.Bo.Exceptions;
using Alpha.Bo.Enums;
using Dapper;
using Alpha.DbAccess;

namespace Alpha.Service.Services
{
    public class AuthonticationService : BaseService, IAuthontication
    {
        IUnitOfWork uow;
        public AuthonticationService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public async Task ChangePassword(ChangePasswordBo item)
        {
            try
            {
                var res = this.Login(new UserLoginBo { Email = item.Email, Password = item.Password });
                var userinfo = GetUserInfo(res.UserId);
                userinfo.Password = item.NewPassword;
                await uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        private User GetUserInfo(Guid userid)
        {
            try
            {
                return this.uow.Context.Users.FirstOrDefault(p => p.UserId == userid) ?? throw new NullReferenceException();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public SessionBo Login(UserLoginBo item)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    var res = cn.Query<User>(@"select * from [User] where Email = @Email and Password = @Password",
                        new { Email = item.Email.Trim().ToLower(), Password = item.Password }).FirstOrDefault();
                    if (res is null)
                    {
                        throw new InvaliedUserInputsException("invalied login");
                    }
                    return new SessionBo
                    {
                        Id = res.Id,
                        Name = res.Name,
                        UserId = res.UserId,
                        Email = res.Email,
                        Country = res.Country,
                        Sex = (int)res.Gender,
                        MaritalStatus = (int)res.MaritalStatus,
                        ProfileImage = res.ProfileImage,
                        IsStater = res.IsStarter
                    };
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<SessionBo> Register(UserBo item)
        {
            try
            {
                item.RegDate = DateTime.UtcNow;
                item.Email = item.Email.Trim().ToLower();
                item.UserId = Guid.NewGuid();
                item.Gender = Enums.Gender.None;
                item.IsValiedEmail = false;
                item.MaritalStatus = (int)Enums.MaritalStatus.Not_selected;
                item.Country = -1;//not mention
                item.Dob = DateTime.Now.AddYears(-18);
                item.ProfileImage = "no.jpg";
                item.IsStarter = true;
                var user = Mapper.Map<User>(item);
                this.uow.Context.Users.Add(user);
                await this.uow.SaveAsync();
                return new SessionBo
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserId = user.UserId,
                    Email = item.Email,
                    IsStater = true
                };
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<string[]> ForgetPasswordRequest(string email)
        {
            try
            {
                var result = this.uow.Context.Users.FirstOrDefault(p => p.Email.Equals(email.Trim().ToLower()));
                if (result is null)
                {
                    throw new ObjectNotFoundException();
                }
                var random = Bo.Utility.Helper.GenarateRandomNumber(5);
                result.Token = $"{Enums.TokenType.ForgetPassword}-{random}";
                await uow.SaveAsync();
                return new string[] { random, result.Name };
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<SessionBo> ForgetPasswordRequestTokenValidate(string token, string email)
        {
            try
            {
                var result = this.uow.Context.Users.FirstOrDefault(p => p.Email.Equals(email.Trim().ToLower()));
                if (result is null)
                {
                    throw new ObjectNotFoundException();
                }
                try
                {
                    var tokentype = result.Token.Split('-')[0].Trim();
                    var _token = result.Token.Split('-')[1].Trim();
                    if (!tokentype.Equals(Enums.TokenType.ForgetPassword.ToString()))
                    {
                        throw new InvaliedTokenException();
                    }
                    else if (!_token.Trim().Equals(token))
                    {
                        throw new InvaliedTokenException();
                    }
                    else
                    {
                        return new SessionBo
                        {
                            Email = result.Email,
                            Id = result.Id,
                            Name = result.Name,
                            UserId = result.UserId
                        };
                    }
                }
                catch
                {
                    throw new InvaliedTokenException();
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task ChangePassword(string password, Guid userid)
        {
            try
            {
                this.uow.UserRepository.GetByID(userid).Password = password;
                await this.uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task StartCriends(Guid userid)
        {
            try
            {
                var r = this.uow.Context.Users.Where(p => p.UserId == userid).FirstOrDefault();
                if (r == null)
                {
                    throw new ObjectNotFoundException();
                }
                r.IsStarter = false;
                await this.uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
    }
}
