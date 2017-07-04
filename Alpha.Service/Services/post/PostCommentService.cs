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
                var r = this.uow.Context.PostComments.FirstOrDefault(p => p.PostId == id && p.UserId == userid);
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

        public async Task<List<PostCommentSearchResponse>> Search(Guid postid)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    return cn.Query<PostCommentSearchResponse>(@"select [PostComment].Comment,
                            [PostComment].CommentDate,[PostComment].CommentId,
                            [PostComment].UserId,[User].Name,[User].ProfileImage
                            from [PostComment]
                            inner join [User] on [User].UserId = [PostComment].UserId
                            where [PostComment].PostId = @PostId", new { PostId = postid }).ToList();
                }
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
