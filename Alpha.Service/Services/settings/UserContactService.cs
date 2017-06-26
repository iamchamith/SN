using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Bo.Bo;
using Alpha.Bo.Enums;
using Alpha.Service.Infrastructure;
using AutoMapper;
using Alpha.Poco;
using Alpha.Service.Services.settings;

namespace Alpha.Service.Services
{
    public class UserContactService : BaseService, IUserContact
    {
        IUnitOfWork uow;
        public UserContactService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }
        public async Task Delete(Enums.SocialNetworks id, Guid userid)
        {
            try
            {
                var r = this.Read(id, userid);
                this.uow.UserContactRepository.Delete(r.Result.Id);
                await this.uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<UserContactBo> Insert(UserContactBo item)
        {
            try
            {
                var r = Mapper.Map<UserContact>(item);
                this.uow.UserContactRepository.Insert(r);
                await this.uow.SaveAsync();
                return Mapper.Map<UserContactBo>(r);
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<List<UserContactBo>> Read(Guid userid)
        {
            try
            {
                var result = (from x in this.uow.UserContactRepository.Get(p => p.UserId == userid)
                              orderby x.Key descending
                              select new { Key = x.Key, Value = x.Url, SocialNetwork = x.SocialNetwork }).ToList();
                var r = new List<UserContactBo>();
                var isview = LookupsService.SocialNetworoksShowValues();
                foreach (var item in result)
                {
                    var isvisible = false;
                    if (isview.Any(p => p == (int)item.SocialNetwork))
                    {
                        isvisible = true;
                    }
                    r.Add(new UserContactBo
                    {
                        Url = item.Value,
                        Key = item.Key,
                        Icon = $"{item.SocialNetwork.ToString()}.png",
                        IsShowValue = isvisible,
                        SocialNetwork = item.SocialNetwork
                    });
                }
                return r.OrderBy(p => p.Key).ToList();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public Task<UserContactBo> Read(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<UserContactBo> Read(Enums.SocialNetworks id, Guid userid)
        {
            try
            {
                return Mapper.Map<UserContactBo>(this.uow.Context.Contacts.FirstOrDefault(p => p.UserId == userid &&
                 p.SocialNetwork == id));

            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task Update(UserContactBo item)
        {
            try
            {
                var r = await Read(item.SocialNetwork, item.UserId);
                if (r is null)
                {
                    await this.Insert(item);
                }
                else
                {
                    var obj = Mapper.Map<UserContact>(r);
                    obj.Url = item.Url;
                    obj.Key = item.Key;
                    this.uow.UserContactRepository.Update(obj);
                    await this.uow.SaveAsync();
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public IEnumerable<DropDownBo> Read(bool enums = true)
        {
            try
            {
                var socialnetworks = new List<DropDownBo>();
                foreach (Enums.SocialNetworks val in Enum.GetValues(typeof(Enums.SocialNetworks)))
                {
                    socialnetworks.Add(new DropDownBo
                    {
                        Value = ((int)val).ToString(),
                        Text = val.ToString().Replace("_", " ")
                    });
                }
                return socialnetworks.OrderBy(p => p.Text);
            }
            catch (Exception e) { throw ExceptionHandler(e); }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserContactBo>> Read()
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, Guid userid)
        {
            throw new NotImplementedException();
        }
    }
}
