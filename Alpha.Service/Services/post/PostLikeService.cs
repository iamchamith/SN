﻿using Alpha.Service.Infrastructure;
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
        public async Task LikeDislikePost(Guid userid, Guid postid, PostLikeType postLikeType, bool islike)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    var response = cn.Query<PostLike>(@"select PostLikeType,PostId,UserId,Id from PostLikes
                    where UserId = @UserId and PostId = @PostId", new { UserId = userid, PostId = postid }).FirstOrDefault();
                    var isfirst = false;
                    var islikex = false;
                    var obj = new PostLike();
                    obj.PostId = postid;
                    obj.UserId = userid;
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
                                ress.Likes = 1;
                                ress.Dislikes = 0;
                            }
                            else
                            {
                                ress.Likes = 0;
                                ress.Dislikes = 1;
                            }
                        }
                        else
                        {
                            if (postLikeType == PostLikeType.Like)
                            {
                                if (islike)
                                {
                                    ress.Likes = ress.Likes + 1;
                                    ress.Dislikes = ress.Dislikes - 1;
                                }
                                else {
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
    }
}
