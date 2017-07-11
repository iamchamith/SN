using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Bo.Bo.posts;
using Alpha.Service.Infrastructure;
using Dapper;
using Alpha.DbAccess;
using AutoMapper;
using Alpha.Poco;

namespace Alpha.Service.Services.post
{
    public class NotificationService :BaseService, INotificationService
    {
        IUnitOfWork uow;
        public NotificationService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }
        public async Task Delete(Guid id)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    cn.Execute("delete from notifications where ToUser = @UserId", new { UserId = id });
                }   
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<NotificationBo> Insert(NotificationBo item)
        {
            try
            {
                var poco = Mapper.Map<Notification>(item);
                this.uow.NotificationRepository.Insert(poco);
                await this.uow.SaveAsync();
                return Mapper.Map<NotificationBo>(poco);
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
        public Task Delete(Guid id, Guid userid)
        {
            throw new NotImplementedException();
        }
        public Task<List<NotificationBo>> Read()
        {
            throw new NotImplementedException();
        }

        public Task<NotificationBo> Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(NotificationBo item)
        {
            throw new NotImplementedException();
        }

        Task<List<NotificationBo>> IRepository<NotificationBo, Guid>.Read(Guid userid)
        {
            throw new NotImplementedException();
        }
    }
}
