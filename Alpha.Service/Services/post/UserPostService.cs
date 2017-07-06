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
using System.Data.Common;
using System.Data;
using Alpha.Service.Services.post;

namespace Alpha.Service.Services
{
    public class UserPostService : BaseService, IUserPost
    {
        IUnitOfWork uow;
        Interfaces.IPost servicePost;
        IPostCommentService servicePostComment;
        public UserPostService(IUnitOfWork _uow)
        {
            this.uow = _uow;
            this.servicePost = new PostService(this.uow);
            this.servicePostComment = new PostCommentService(this.uow);
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
                var x = this.uow.Context.Posts.FirstOrDefault(p => p.PostId == id);
                if (x != null)
                {
                    this.uow.Context.Posts.Remove(x);
                }
                if (x.PostType == Bo.Enums.Enums.PostType.Comment)
                {
                    var removefromposttype = this.uow.Context.PostNeedComments.FirstOrDefault(p => p.PostId == id);
                    if (removefromposttype != null)
                    {
                        this.uow.Context.PostNeedComments.Remove(removefromposttype);
                    }
                }
                else if (x.PostType == Bo.Enums.Enums.PostType.Poll)
                {
                    var removefromposttype = this.uow.Context.PostPolls.FirstOrDefault(p => p.PostId == id);
                    if (removefromposttype != null)
                    {
                        this.uow.Context.PostPolls.Remove(removefromposttype);
                    }
                }
                else if (x.PostType == Bo.Enums.Enums.PostType.Question)
                {
                    var removefromposttype = this.uow.Context.PostQuestion.FirstOrDefault(p => p.PostId == id);
                    if (removefromposttype != null)
                    {
                        this.uow.Context.PostQuestion.Remove(removefromposttype);
                    }
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
                var where = new StringBuilder();
                var posttypeseach = new StringBuilder();
                where.Append(" where ");
                var isWhere = false;
                var isposttypesearch = false;
                if (!string.IsNullOrEmpty(request.Topic))
                {
                    where.Append($"	 Post.Topic Like '{request.Topic}%'   and   ");
                    isWhere = true;
                }
                if (request.PostSearchType != Bo.Enums.Enums.PostSearchType.Feed)
                {
                    where.Append($" UserPost.UserId Like '{request.UserId}'   and   ");
                    isWhere = true;
                }
                if (request.IsNeedComments)
                {
                    posttypeseach.Append($" Post.PostType = '{(int)Bo.Enums.Enums.PostType.Comment}'   or   ");
                    isposttypesearch = true;
                }
                if (request.IsPoll)
                {
                    posttypeseach.Append($" Post.PostType = '{(int)Bo.Enums.Enums.PostType.Poll}'   or   ");
                    isposttypesearch = true;
                }
                if (request.IsQuestions)
                {
                    posttypeseach.Append($" Post.PostType = '{(int)Bo.Enums.Enums.PostType.Question}'   or   ");
                    isposttypesearch = true;
                }
                var _where = string.Empty;
                if (isWhere)
                {
                    _where = where.ToString().Substring(0, where.ToString().Length - 8);
                }
                if (isposttypesearch)
                {
                    _where += $@"
                       {(!isWhere ? " WHERE " : " AND ")} ({posttypeseach.ToString().Substring(0, posttypeseach.ToString().Length - 8)})";
                }
                sql.Append($@"select UserPost.Id as [userPostId],UserPost.PostId,[User].Name,
                    [User].Email,[User].UserId,
                    UserPost.Anonymous,UserPost.PostDate,Post.PostType,Post.Topic,post.Tags,
                    [User].ProfileImage,UserPost.Likes,UserPost.Dislikes from [User]
                    inner join UserPost on [User].UserId = UserPost.UserId
                    inner join Post on Post.PostId = UserPost.PostId {_where}
                    order by UserPost.PostDate {(request.IsDateDesc ? "Desc" : "Asc")}
                    OFFSET @Skip ROWS 
                    FETCH NEXT @Take ROWS ONLY;");
                using (var cn = DatabaseInfo.Connection)
                {
                    var r = cn.Query<UserPostSearchResponse>(sql.ToString(), new { Skip = request.Skip, Take = request.Take }).ToList();
                    var postidlist = r.Select(p => p.PostId).ToList();
                    var askQuestions = await SearchAskQuestion(postidlist, cn);
                    var askPoll = await SearchAskPoll(postidlist, cn);
                    var askNeedComment = await SearchAskNeedComment(postidlist, cn);
                    var myPostStatus = await MyPostStatus(postidlist, request.MyUserId, cn);
                    var postcomments = await this.servicePostComment.Search(postidlist, request.MyUserId, cn);
                    foreach (var item in r)
                    {
                        if (item.PostType == Bo.Enums.Enums.PostType.Question)
                        {
                            item.PostQuestion = Mapper.Map<PostQuestionBo>(askQuestions.FirstOrDefault(p => p.PostId == item.PostId) ?? new PostQuestion());
                        }
                        else if (item.PostType == Bo.Enums.Enums.PostType.Poll)
                        {
                            item.PostPoll = Mapper.Map<PostPollBo>(askPoll.FirstOrDefault(p => p.PostId == item.PostId) ?? new PostPoll());
                        }
                        else
                        {
                            item.PostNeedComment = Mapper.Map<PostNeedCommentBo>(askNeedComment.FirstOrDefault(p => p.PostId == item.PostId) ?? new PostNeedComment());
                        }
                        item.ProfileImage = base.ImageProfileBlobPrefix + item.ProfileImage;

                        var c = myPostStatus.FirstOrDefault(p => p.PostId == item.PostId);
                        item.PostDayShow = base.DateShow(item.PostDate);
                        item.MeLike = c.MeLike;
                        item.MeDislike = c.MeDislike;
                        item.MeComment = c.MeComment;
                        item.PostCommentSearchResponse = postcomments.Where(p => p.PostId == item.PostId).OrderBy(p => p.CommentDate).ToList();
                    }
                    return r;
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        async Task<List<PostQuestion>> SearchAskQuestion(List<Guid> postId, IDbConnection cn)
        {
            try
            {
                return cn.Query<PostQuestion>(@"select  PostId,Topic,Description from PostQuestions where PostId in @PostId", new { PostId = postId }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<List<PostPoll>> SearchAskPoll(List<Guid> postId, IDbConnection cn)
        {
            try
            {
                var res = cn.Query<PostPoll>($@"
                select  PostId,Topic,Vs1Url,Vs2Url from PostPolls where PostId in @PostId", new { PostId = postId }).ToList();

                foreach (var item in res)
                {
                    item.Vs1Url = base.ImagePostBlobPrefix + item.Vs1Url;
                    item.Vs2Url = base.ImagePostBlobPrefix + item.Vs2Url;
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<List<PostNeedComment>> SearchAskNeedComment(List<Guid> postId, IDbConnection cn)
        {
            try
            {
                var res = cn.Query<PostNeedComment>(@"select PostId,Topic,Description,ImageUrl from PostNeedComments where PostId in @PostId", new { PostId = postId }).ToList();
                foreach (var item in res)
                {
                    if (string.IsNullOrEmpty(item.ImageUrl))
                    {
                        item.IsImage = false;
                        item.ImageUrl = "";
                    }
                    else
                    {
                        item.IsImage = true;
                        item.ImageUrl = base.ImagePostBlobPrefix + item.ImageUrl;
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<List<PostMyStatus>> MyPostStatus(List<Guid> postId, Guid userid, IDbConnection cn)
        {
            try
            {
                var result = cn.Query<PostLike>("select PostId,PostLikeType from PostLikes where PostId in @PostId and UserId = @UserId",
                    new { PostId = postId, UserId = userid });
                var res = new List<PostMyStatus>();
                foreach (var item in postId)
                {
                    var r = result.FirstOrDefault(p => p.PostId == item);
                    if (r is null)
                    {
                        res.Add(new PostMyStatus
                        {
                            MeComment = false,
                            MeDislike = false,
                            MeLike = false,
                            PostId = item
                        });
                    }
                    else
                    {
                        res.Add(new PostMyStatus
                        {
                            MeComment = true,
                            MeLike = (r.PostLikeType == Bo.Enums.Enums.PostLikeType.Like) ? true : false,
                            MeDislike = (r.PostLikeType == Bo.Enums.Enums.PostLikeType.Dislike) ? true : false,
                            PostId = item
                        });
                    }
                }
                return res;
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
