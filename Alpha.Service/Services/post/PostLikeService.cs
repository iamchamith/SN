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
using Alpha.Bo.Bo;
using Alpha.Bo.Enums;
using Alpha.Bo.Bo.posts;

namespace Alpha.Service.Services.post
{
    public class PostLikeService : BaseService, IPostLikeService
    {
        IUnitOfWork uow;
        IConnectCriends connectCriends;
        public PostLikeService(IUnitOfWork _uow)
        {
            this.uow = _uow;
            connectCriends = new ConnectCriendsService(this.uow);
        }
        public async Task LikeDislikePost(Guid userid, Guid postid, PostLikeType postLikeType, PostLikeModeType modeType, bool islike)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    var response = cn.Query<PostLike>(@"select PostLikeType,PostId,UserId,Id from PostLikes
                    where UserId = @UserId and PostId = @PostId and PostLikeModeType = @PostLikeModeType ",
                    new { UserId = userid, PostId = postid, PostLikeModeType = modeType }).FirstOrDefault();
                    var isfirst = false;
                    var islikex = false;
                    var obj = new PostLike();
                    obj.PostId = postid;
                    obj.UserId = userid;
                    obj.PostLikeModeType = modeType;
                    if (response is null && islike)
                    {
                        obj.PostLikeType = postLikeType;
                        this.uow.PostLikeRepository.Insert(obj);
                        isfirst = true;
                    }
                    else
                    {
                        if (islike)
                        {
                            response.PostLikeType = postLikeType;
                            this.uow.PostLikeRepository.Update(response);
                        }
                        else
                        {
                            this.uow.PostLikeRepository.Delete(response);
                        }
                    }
                    //update post like dislike count
                    var ress = this.uow.Context.UserPosts.FirstOrDefault(p => p.PostId == postid);
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
                                ress.Likes = ress.Likes + 1;
                            }
                            else
                            {
                                ress.Dislikes = ress.Dislikes + 1;
                            }
                        }
                        else
                        {
                            if (postLikeType == PostLikeType.Like)
                            {
                                if (islike)
                                {
                                    ress.Likes = ress.Likes + 1;
                                }
                                else
                                {
                                    ress.Likes = ress.Likes - 1;
                                }
                            }
                            else
                            {
                                if (islike)
                                {
                                    ress.Likes = ress.Likes - 1;
                                    ress.Dislikes = ress.Dislikes + 1;
                                }
                                else
                                {
                                    ress.Dislikes = ress.Dislikes - 1;
                                }
                            }
                        }
                        this.uow.UserPostRepository.Update(ress);
                    }
                    await this.uow.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler(ex);
            }
        }

        public async Task<WhoLikeDislikeDoBo> WhoLikeDislikeDo(Guid postid, Guid userid, PostLikeType postLikeType)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    var result = cn.Query<UserPostLikeDislikeBo>($@"select UserId, IsAnonymas from postlikes where PostId = @PostId
                    and PostLikeType ='{(int)(postLikeType)}'",
                    new { PostId = postid }).ToList();
                    var criends = await this.connectCriends.Search(result.Select(p => p.UserId).ToList(), userid, cn);
                    var useridsAnonymas = result.Where(p => p.IsAnonymas).Select(p => p.UserId).ToList();
                    criends.RemoveAll(p => useridsAnonymas.Contains(p.UserId));
                    return new WhoLikeDislikeDoBo
                    {
                        Criends = criends,
                        AnonymasCount = useridsAnonymas.Count
                    }; 
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler(ex);
            }
        }
    }
}
