using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Bo;
using Alpha.Service.Infrastructure;
using Alpha.Bo.Exceptions;
using AutoMapper;
using Alpha.Poco;
using Alpha.Bo.Bo.posts;
using Dapper;
using Alpha.DbAccess;
using System.Data;

namespace Alpha.Service.Services.post
{
    public class PostCommentService : BaseService, IPostCommentService
    {
        IUnitOfWork uow;
        public PostCommentService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }
        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid id, Guid userid)
        {
            try
            {
                var r = this.uow.Context.PostComments.FirstOrDefault(p => p.CommentId == id && p.UserId == userid);
                if (r is null)
                {
                    throw new ObjectNotFoundException();
                }
                this.uow.Context.PostComments.Remove(r);
                await this.uow.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler(ex);
            }
        }

        public async Task<PostCommentBo> Insert(PostCommentBo item)
        {
            try
            {
                item.CommentId = Guid.NewGuid();
                var r = Mapper.Map<PostComment>(item);
                this.uow.PostCommentRepository.Insert(r);
                await this.uow.SaveAsync();
                return Mapper.Map<PostCommentBo>(r);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler(ex);
            }
        }

        public Task<List<PostCommentBo>> Read()
        {
            throw new NotImplementedException();
        }

        public Task<PostCommentBo> Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PostCommentSearchResponse>> Search(Guid postid,Guid userid)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    var r =  cn.Query<PostCommentSearchResponse>(@"select [PostComment].Comment,
                            [PostComment].CommentDate,[PostComment].CommentId,
                            [PostComment].UserId,[User].Name,[User].ProfileImage,
                            [PostComment].PostId
                            from [PostComment]
                            inner join [User] on [User].UserId = [PostComment].UserId
                            where [PostComment].PostId = @PostId 
                            order by [PostComment].CommentDate asc", new { PostId = postid }).ToList();
                    foreach (var item in r)
                    {
                        item.CommentDateStr = base.DateShow(item.CommentDate);
                        item.ProfileImage = base.ImageProfileBlobPrefix + item.ProfileImage;
                        item.IsMine = item.UserId == userid;
                    }
                    return r;
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<List<PostCommentSearchResponse>> Search(List<Guid> postid, Guid userid,IDbConnection cn)
        {
            try
            {
                    var r = cn.Query<PostCommentSearchResponse>(@"select [PostComment].Comment,
                            [PostComment].CommentDate,[PostComment].CommentId,
                            [PostComment].UserId,[User].Name,[User].ProfileImage,
                            [PostComment].PostId
                            from [PostComment]
                            inner join [User] on [User].UserId = [PostComment].UserId
                            where [PostComment].PostId in @PostId 
                            order by [PostComment].CommentDate asc", new { PostId = postid }).ToList();
                    foreach (var item in r)
                    {
                        item.CommentDateStr = base.DateShow(item.CommentDate);
                        item.ProfileImage = base.ImageProfileBlobPrefix + item.ProfileImage;
                        item.IsMine = item.UserId == userid;
                    }
                    return r;
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public Task Update(PostCommentBo item)
        {
            throw new NotImplementedException();
        }

        Task<List<PostCommentBo>> IRepository<PostCommentBo, Guid>.Read(Guid userid)
        {
            throw new NotImplementedException();
        }
    }
}
