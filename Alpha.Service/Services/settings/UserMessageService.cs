using Alpha.Service.Infrastructure;
using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Bo.Bo.settings;
using AutoMapper;
using Alpha.Poco;

namespace Alpha.Service.Services.settings
{
    public class UserMessageService : BaseService, IUserMessageService
    {
        IUnitOfWork uow;
        public UserMessageService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public List<UserMessageBo> Read(UserMessageSendRequestBo item)
        {
            try
            {
                return this.uow.UserMessageRepository.Get(p => p.FromUser == item.FromUser &&
                p.ToUser == item.ToUser).OrderByDescending(p => p.SendDate).Skip(item.Skip)
                .Take(item.Take).ToList()
                .Select(x => Mapper.Map<UserMessageBo>(x)).ToList();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<UserMessageBo> Send(UserMessageBo item)
        {
            try
            {
                item.SendDate = DateTime.UtcNow;
                var r = Mapper.Map<UserMessage>(item);
                this.uow.UserMessageRepository.Insert(r);
                await this.uow.SaveAsync();
                return Mapper.Map<UserMessageBo>(r);
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
    }
}
