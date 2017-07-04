using Alpha.Service.Infrastructure;
using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Alpha.DbAccess;
using static Alpha.Bo.Enums.Enums;
using Alpha.Poco;
using Alpha.Bo.Exceptions;

namespace Alpha.Service.Services.post
{
    public class PostLikeService : BaseService, IPostLikeService
    {
        IUnitOfWork uow;
        public PostLikeService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }
        public async Task LikeDislikePost(Guid userid, Guid postid, PostLikeType postLikeType, bool islike, bool isdislike)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    var response = cn.Query<PostLike>(@"select IsLike,IsDisLike,PostId,UserId,Id from PostLikes
                    where UserId = @UserId and PostId = @PostId", new { UserId = userid, PostId = postid }).FirstOrDefault();
                    var isfirst = false;
                    var islikex = false;
                    var obj = new PostLike();
                    obj.PostId = postid;
                    obj.UserId = userid;
                    if (response is null)
                    {
                        if (postLikeType == PostLikeType.Like)
                        {
                            obj.IsLike = true;
                            obj.IsDisLike = false;
                        }
                        else
                        {
                            obj.IsLike = false;
                            obj.IsDisLike = true;
                        }
                        this.uow.PostLikeRepository.Insert(obj);
                        isfirst = true;
                    }
                    else
                    {
                        if (response.IsDisLike)
                        {
                            islikex = false;
                        }
                        else
                        {
                            islikex = true;
                        }

                        if (postLikeType == PostLikeType.Like)
                        {
                            response.IsLike = true;
                            response.IsDisLike = false;
                        }
                        else
                        {
                            response.IsLike = false;
                            response.IsDisLike = true;
                        }
                        this.uow.PostLikeRepository.Update(obj);
                    }
                    var ress = this.uow.Context.UserPosts.FirstOrDefault(p => p.UserId == userid && p.PostId == postid);
                    if (ress is null)
                    {
                        throw new ObjectNotFoundException();
                    }
                    else
                    {
                        if (isfirst)
                        {
                            if (postLikeType == PostLikeType.Like)
                            {
                                ress.Like = 1;
                                ress.Dislike = 0;
                            }
                            else
                            {
                                ress.Like = 0;
                                ress.Dislike = 1;
                            }
                        }
                        else
                        {
                            if (postLikeType == PostLikeType.Like)
                            {
                                ress.Like = ress.Like + 1;
                                ress.Dislike = ress.Dislike - 1;
                            }
                            else
                            {
                                ress.Like = ress.Like - 1;
                                ress.Dislike = ress.Dislike + 1;
                            }
                        }
                    }
                    await this.uow.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler(ex);
            }
        }
    }
}
