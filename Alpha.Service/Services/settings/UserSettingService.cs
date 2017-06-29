using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Bo;
using Alpha.Service.Infrastructure;
using AutoMapper;
using Alpha.Bo.Enums;
using Alpha.Bo.Utility;
using Alpha.Bo.Exceptions;

namespace Alpha.Service.Services
{
    public class UserSettingService : BaseService, IUserSettings
    {
        IUnitOfWork uow;
        public UserSettingService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserBo> Insert(UserBo item)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserBo>> Read()
        {
            throw new NotImplementedException();
        }

        public async Task<UserBo> Read(Guid id)
        {
            try
            {
                var item = Mapper.Map<UserBo>(this.uow.UserRepository.GetByID(id));
                item.ProfileImage = $"{Bo.Utility.Configs.ImagePrefixBlob}{Bo.Enums.Enums.Imagetype.profileimages}/" + item.ProfileImage;
                item.Password = string.Empty;
                return item;
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task Update(UserBo item)
        {
            try
            {
                var user = this.uow.UserRepository.GetByID(item.UserId);
                user.MaritalStatus = (Enums.MaritalStatus)item.MaritalStatus;
                user.Name = item.Name;
                user.Gender = (Enums.Gender)item.Gender;
                user.Country = item.Country;
                user.Bio = item.Bio;
                user.Dob = item.Dob;
                user.IsValiedEmail = item.IsValiedEmail;
                user.Employeement = item.Employeement;
                await this.uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<string> SendValidateEmailToken(Guid userid)
        {
            try
            {
                var random = Helper.GenarateRandomNumber(5);
                var result = this.uow.UserRepository.GetByID(userid);
                result.Token = $"{Enums.TokenType.EmailValidate}-{random}";
                await this.uow.SaveAsync();
                return random;
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task ValidateEmailToken(string token, Guid userid)
        {
            try
            {
                var result = this.uow.UserRepository.GetByID(userid);
                var dbtoken = string.Empty;
                var tokentype = string.Empty;
                try
                {
                    tokentype = result.Token.Split('-')[0];
                    dbtoken = result.Token.Split('-')[1];
                    if (tokentype != Enums.TokenType.EmailValidate.ToString())
                    {
                        throw new InvaliedTokenException("invalied email validation token");
                    }
                }
                catch (Exception)
                {
                    throw new InvaliedTokenException("invalied email validation token");
                }
                if (dbtoken.ToString().Trim().Equals(token.Trim()))
                {
                    result.Token = string.Empty;
                    result.IsValiedEmail = true;
                }
                else
                {
                    throw new InvaliedTokenException();
                }
                await this.uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        Task<List<UserBo>> IRepository<UserBo, Guid>.Read(Guid userid)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id, Guid userid)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProfileImage(Guid userid, string profileimage)
        {
            try
            {
                var r = this.uow.UserRepository.GetByID(userid);
                r.ProfileImage = profileimage;
                await this.uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
    }
}
