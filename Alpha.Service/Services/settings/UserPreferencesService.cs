using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Bo.Bo;
using Alpha.Service.Infrastructure;
using Dapper;
using Alpha.DbAccess;
using Alpha.Poco;
using Alpha.Bo.Enums;
using AutoMapper;
namespace Alpha.Service.Services
{
    public class UserPreferencesService : BaseService, IUserPreferencesService
    {
        IUnitOfWork uow;
        public UserPreferencesService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id, Guid userid)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    cn.Execute(@"delete from UserPreferences where UserId = @UserId",
                        new { UserId = userid });
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public Task<UserPreferencesBo> Insert(UserPreferencesBo item)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserPreferencesBo>> Read()
        {
            throw new NotImplementedException();
        }

        public Task<UserPreferencesBo> Read(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserPreferencesBo>> Read(Guid userid)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    var result = cn.Query<UserPreferences>(@"select Id,UserId,UserPreference,State from [UserPreferences]
                        where UserId = @UserId", new { UserId = userid }).ToList();
                    return GetPreferences(userid, result);
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        List<UserPreferencesBo> GetPreferences(Guid userid, List<UserPreferences> result)
        {
            var preference = new List<UserPreferencesBo>();
            foreach (Enums.UserPreferencesInfo foo in Enum.GetValues(typeof(Enums.UserPreferencesInfo)))
            {
                var r = result.FirstOrDefault(p => p.UserPreference == foo && p.UserId == userid);
                if (r is null)
                {
                    preference.Add(new UserPreferencesBo
                    {
                        Id = 0,
                        State = true,
                        UserId = userid,
                        UserPreference = foo
                    });
                }
                else
                {
                    preference.Add(Mapper.Map<UserPreferencesBo>(r));
                }
            }
            return preference;
        }

        public Task Update(UserPreferencesBo item)
        {
            throw new NotImplementedException();
        }

        public async Task Update(List<UserPreferencesBo> item, Guid userid)
        {
            try
            {
                var r = item.Select(x => AutoMapper.Mapper.Map<UserPreferences>(x)).ToList();
                foreach (var obj in r)
                {
                    this.uow.UserPreferencesRepository.Update(obj);
                }
                await this.uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
    }
}
