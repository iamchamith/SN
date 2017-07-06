using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Bo;
using Alpha.Service.Infrastructure;
using AutoMapper;
using Alpha.Poco;
using Alpha.Bo.Exceptions;
using Dapper;
using Alpha.DbAccess;
using Alpha.Bo.Bo;

namespace Alpha.Service.Services
{
    public class UserTagsService : BaseService, IUserTags
    {
        IUnitOfWork uow;
        ITags tagService;
        public UserTagsService(IUnitOfWork _uow)
        {
            this.uow = _uow;
            this.tagService = new TagService(this.uow);
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task Delete(int id, Guid userid)
        {
            try
            {
                var r = this.uow.Context.UserTags.FirstOrDefault(p => p.UserId == userid && p.TagId == id);
                if (r is null)
                {
                    throw new ObjectNotFoundException();
                }
                this.uow.Context.UserTags.Remove(r);
                await this.tagService.Update(id, false);
                await this.uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<UserTagBo> Insert(UserTagBo item)
        {
            try
            {
                var r = Mapper.Map<UserTag>(item);
                this.uow.UserTagRepository.Insert(r);
                await this.uow.SaveAsync();
                await this.tagService.Update(r.TagId, true);
                return Mapper.Map<UserTagBo>(r);
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public Task<List<UserTagBo>> Read()
        {
            throw new NotImplementedException();
        }

        public Task<UserTagBo> Read(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserTagBo>> Read(Guid userid)
        {
            try
            {
                var r = (from tags in this.uow.Context.Tags
                         join utags in this.uow.Context.UserTags
                         on tags.Id equals utags.TagId
                         where utags.UserId == userid
                         select new { Text = tags.TagName, Value = tags.Id, Id = utags.Id, Cout = tags.TagCount }).ToList();

                var lst = new List<UserTagBo>();
                foreach (var item in r)
                {
                    lst.Add(new UserTagBo
                    {
                        Id = item.Id,
                        TagId = item.Value,
                        TagName = $"{item.Text} ({item.Cout})"
                    });
                }
                return lst;
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<TagInfoBo> Read(Guid userid, int id)
        {
            try
            {
                var sql = new List<string>();
                sql.Add(@"select Tag.TagName,Tag.Id,Tag.Description,Tag.Owner,[User].Name,[User].Email from Tag
                    inner join[User] on[User].UserId = Tag.Owner
                     where Tag.Id = @Id");
                sql.Add(" select COUNT(*) as c from UserTag where  UserId = @UserId and TagId = @Id ");

                using (var cn = DatabaseInfo.Connection)
                {
                    var result = cn.QueryMultiple(string.Join(";", sql), new { Id = id, UserId = userid });
                    var tag = result.Read<dynamic>().FirstOrDefault();
                    if (tag is null)
                    {
                        throw new ObjectNotFoundException();
                    }
                    var count = result.Read<int>().FirstOrDefault();
                    return new TagInfoBo
                    {
                        Description = tag.Description,
                        TagId = tag.Id,
                        TagName = tag.TagName,
                        IsTagThere = (count == 1),
                        OwnerEmail = tag.Email,
                        OwnerName = tag.Name,
                        OwnerProfileImage = "",
                        OwnerId = tag.Owner
                    };
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
        public Task Update(UserTagBo item)
        {
            throw new NotImplementedException();
        }
    }
}
