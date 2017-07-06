using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using Alpha.Bo;
using Alpha.Service.Infrastructure;
using AutoMapper;
using Alpha.Poco;
using Alpha.Bo.Bo;
using Alpha.DbAccess;
using Alpha.Bo.Exceptions;

namespace Alpha.Service.Services
{
    public class TagService : BaseService, ITags
    {
        IUnitOfWork uow;
        public TagService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public async Task<TagBo> Insert(TagBo item)
        {
            try
            {
                item.TagName = item.TagName.Trim().ToLower();
                item.CreatedDate = DateTime.UtcNow;
                item.TagCount = 1;
                var obj = Mapper.Map<Tag>(item);
                this.uow.TagRepository.Insert(obj);
                await uow.SaveAsync();
                return Mapper.Map<TagBo>(obj);
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<List<DropDownBo>> Read(string q, Guid userid)
        {
            try
            {
                var sql = $@"select CONVERT(varchar(10),id) as Value,TagName+' ('+ CONVERT(varchar(10),TagCount)+')' as Text from tag
                        where TagName like '{q.Trim().ToLower()}%'
                        and id not in(select TagId from UserTag where UserId = @UserId)";
                using (var cn = DatabaseInfo.Connection)
                {
                    return cn.Query<DropDownBo>(sql, new { UserId = userid }).AsList();
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
        public Task<List<TagBo>> Read()
        {
            throw new NotImplementedException();
        }

        public Task<TagBo> Read(int id)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Task Update(TagBo item)
        {
            throw new NotImplementedException();
        }

        public Task<List<TagBo>> Read(Guid userid)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, Guid userid)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int tagid, bool isAdd)
        {
            try
            {
                var r = this.uow.TagRepository.GetByID(tagid);
                if (r is null)
                {
                    throw new ObjectNotFoundException();
                }
                var newcount = r.TagCount + (isAdd ? 1 : -1);
                if (newcount >= 0)
                {
                    r.TagCount = newcount;
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
    }
}
