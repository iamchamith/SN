using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Poco;
using Alpha.Bo;
using Alpha.Service.Infrastructure;
using AutoMapper;
using Alpha.Bo.Exceptions;
using Dapper;
using Alpha.DbAccess;
using Alpha.Bo.Bo.posts;

namespace Alpha.Service.Services
{
    public class UserPostService : BaseService, IUserPost
    {
        IUnitOfWork uow;
        Interfaces.IPost servicePost;
        public UserPostService(IUnitOfWork _uow)
        {
            this.uow = _uow;
            servicePost = new PostService(this.uow);
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid id, Guid userid)
        {
            try
            {
                var r = this.uow.Context.UserPosts.FirstOrDefault(p => p.UserId == userid && p.PostId == id);
                if (r is null)
                {
                    throw new ObjectNotFoundException();
                }
                this.uow.Context.UserPosts.Remove(r);
                await this.uow.SaveAsync();
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<UserPostBo> Insert(PostQuestionBo item, UserPostBo userpostinfo)
        {
            try
            {
                item.Tags = "0";
                var postid = await servicePost.Insert(item);
                var up = GetUserPostShareInfo(userpostinfo, postid);
                var x = Mapper.Map<UserPost>(up);
                this.uow.UserPostRepository.Insert(x);
                await this.uow.SaveAsync();
                return null;
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<UserPostBo> Insert(PostNeedCommentBo item, UserPostBo userpostinfo)
        {
            try
            {
                item.Tags = "0";
                var postid = await servicePost.Insert(item);
                var up = GetUserPostShareInfo(userpostinfo, postid);
                var x = Mapper.Map<UserPost>(up);
                this.uow.UserPostRepository.Insert(x);
                await this.uow.SaveAsync();
                return null;
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<UserPostBo> Insert(PostPollBo item, UserPostBo userpostinfo)
        {
            try
            {
                item.Tags = "0";
                var postid = await servicePost.Insert(item);
                var up = GetUserPostShareInfo(userpostinfo, postid);
                var x = Mapper.Map<UserPost>(up);
                this.uow.UserPostRepository.Insert(x);
                await this.uow.SaveAsync();
                return null;
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
        UserPostBo GetUserPostShareInfo(UserPostBo up, Guid postid)
        {
            up.IsPrimaryUser = Bo.Enums.Enums.YesNo.Yes;
            up.PostDate = DateTime.UtcNow;
            up.PostId = postid;
            up.ParentPostId = Guid.Empty;
            return up;
        }
        public Task<UserPostInfoBo> Insert(UserPostInfoBo item)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserPostInfoBo>> Read()
        {
            throw new NotImplementedException();
        }

        public async Task<UserPostInfoBo> Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserPostInfoBo> Read(Guid postId, Guid userid)
        {
            try
            {
                var userpost = this.uow.Context.UserPosts.FirstOrDefault(p => p.PostId == postId && p.UserId == userid);
                if (userpost is null)
                {
                    throw new ObjectNotFoundException();
                }
                var post = await this.servicePost.Read(postId);
                return new UserPostInfoBo
                {
                    Post = Mapper.Map<PostBo>(post),
                    UserPost = Mapper.Map<UserPostBo>(userpost)
                };
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<List<UserPostSearchResponse>> SearchPost(UserPostSearchRequest request)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($@"select userPost.Id as [UserPostId],userPost.PostDate,userPost.Anonymous,
                    userPost.ParentPostId,post.PostId,post.Tags,post.Titile,post.Description,userPost.UserId,
					[user].Email,[user].Name
                    from UserPost as userPost
                    Inner join  Post as post on userPost.PostId = post.PostId
					inner join [User] as [user] on [user].UserId = userPost.UserId 

                    where userPost.UserId = '{request.UserId}'
                    {(!string.IsNullOrEmpty(request.Topic) ? " and post.Titile like '" + request.Topic + "%'" : "")}
                    order by userPost.PostDate {(request.IsDateDesc ? "Desc" : "Asc")}");
                using (var cn = DatabaseInfo.Connection)
                {
                    var r = cn.Query<UserPostSearchResponse>(sql.ToString()).ToList();
                    foreach (var item in r)
                    {
                        item.ProfileImage = $"https://www.gravatar.com/avatar/{Alpha.Bo.Utility.Helper.MD5Hash(item.Email)}";
                    }
                    return r;
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public Task Update(UserPostInfoBo item)
        {
            throw new NotImplementedException();
        }

        Task<List<UserPostInfoBo>> IRepository<UserPostInfoBo, Guid>.Read(Guid userid)
        {
            throw new NotImplementedException();
        }
    }
}
