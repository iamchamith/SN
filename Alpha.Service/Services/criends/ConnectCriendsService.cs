using Alpha.Bo.Bo;
using Alpha.Service.Infrastructure;
using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Alpha.DbAccess;
using Alpha.Bo.Enums;
using Alpha.Poco;
using Alpha.Bo;
using Alpha.Bo.Bo.criends;
using AutoMapper;
using System.Data;
using Alpha.Bo.Bo.posts;

namespace Alpha.Service.Services
{
    public class ConnectCriendsService : BaseService, IConnectCriends
    {
        IUnitOfWork uow;
        public ConnectCriendsService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public async Task AddRemoveRelation(CriendsRelationshipBo item)
        {
            try
            {
                Func<UserRelation, Enums.UserRelationshipStatus, Enums.YesNo, UserRelation> setState = (poco, State, isYes) =>
                {
                    switch (State)
                    {
                        case Enums.UserRelationshipStatus.Block:
                            poco.IsBlock = (isYes == Enums.YesNo.Yes);
                            if (poco.IsBlock)
                            {
                                poco.IsFollower = false;
                                poco.IsFollowing = false;
                            }
                            break;
                        case Enums.UserRelationshipStatus.Follower:
                            poco.IsFollower = (isYes == Enums.YesNo.Yes);
                            if (poco.IsFollower)
                            {
                                poco.IsBlock = false;
                            }
                            break;
                        case Enums.UserRelationshipStatus.Following:
                            poco.IsFollowing = (isYes == Enums.YesNo.Yes);
                            if (poco.IsFollowing)
                            {
                                poco.IsBlock = false;
                            }
                            break;
                        default:
                            break;
                    }
                    return poco;
                };

                //get exsisting tupple
                var r = this.uow.Context.UserRelations.FirstOrDefault(p => p.OwnerId == item.OwnerId && p.UserId == item.UserId);
                if (r is null)
                {
                    var poco = new Poco.UserRelation
                    {
                        OwnerId = item.OwnerId,
                        UserId = item.UserId,
                        IsBlock = false,
                        IsFollowing = false,
                        IsFollower = false
                    };
                    poco = setState(poco, item.State, item.OparationType);
                    // first time add
                    this.uow.Context.UserRelations.Add(poco);
                }
                else
                {
                    r = setState(r, item.State, item.OparationType);
                }
                await this.uow.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler(ex);
            }
        }

        public async Task<List<SearchCriendsResultBo>> Search(SearchCriendsRequestBo item)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append("select UserId,Email,Name,ProfileImage from [User]    WHERE  UserId <> @UserId   AND   ");
                if (!string.IsNullOrEmpty(item.Name))
                {
                    sql.Append($" Name like '{item.Name}%' AND");
                }
                if (item.MaritalStatus != -1)
                {
                    sql.Append($" MaritalStatus = '{item.MaritalStatus}'   AND   ");
                }
                if (item.Country != -1)
                {
                    sql.Append($" Country = '{item.Country}'   AND   ");
                }
                if (item.Sex != -1)
                {
                    sql.Append($" Gender = '{item.Sex}'   AND   ");
                }
                var q = sql.ToString().Substring(0, sql.ToString().Length - 8);
                q += @" order by Name
                        offset @Skip rows 
                        fetch next @Take rows only;";
                using (var cn = DatabaseInfo.Connection)
                {
                    var r = cn.Query<SearchCriendsResultBo>(q, new
                    {
                        UserId = item.OwnerId,
                        Skip = item.Skip,
                        Take = item.Take
                    }).ToList();
                    var query = @"SELECT  top 10 UserTag.UserId,Tag.Id,Tag.TagName FROM Tag
                        INNER JOIN UserTag on Tag.Id = UserTag.TagId
                        where UserTag.UserId in @UserId ;
                        SELECT UserId,IsFollowing,IsFollower,IsBlock from UserRelations where UserId in @UserId
                        AND OwnerId =  @OwnerId; ";
                    var result = cn.QueryMultiple(query
                            , new
                            {
                                UserId = r.Select(p => p.UserId).ToList(),
                                OwnerId = item.OwnerId
                            });
                    var userTags = result.Read<UserTagBo>();
                    var relations = result.Read<CriendsRelationsBo>();
                    foreach (var obj in r)
                    {
                        obj.ProfileImage = $"{Bo.Utility.Configs.ImagePrefixBlob}{Bo.Enums.Enums.Imagetype.profileimages}/{obj.ProfileImage}";
                        obj.UserTags = userTags.Where(p => p.UserId == obj.UserId).ToList() ?? new List<UserTagBo>();
                        obj.Relationships = relations.FirstOrDefault(p => p.UserId == obj.UserId) ?? new CriendsRelationsBo();
                    }
                    return r;
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        private class UserRelationCount
        {
            public bool IsFollowing { get; set; }
            public bool IsFollower { get; set; }
        }
        public async Task<RelationCountBo> GetCriendsRelationCount(Guid userid)
        {
            try
            {
                using (var cn = DatabaseInfo.Connection)
                {
                    var response = await cn.QueryMultipleAsync(@"select IsFollowing, IsFollower from UserRelations
                      where OwnerId = @UserId and IsBlock <> 1;
                    select  IsFollowing,IsFollower from UserRelations where UserId = @UserId and IsBlock <> 1",
                            new { UserId = userid });

                    var my = response.Read<UserRelationCount>();
                    var criends = response.Read<UserRelationCount>();

                    return new RelationCountBo
                    {
                        MyFollowers = my.Count(p => p.IsFollower),
                        MyFollowing = my.Count(p => p.IsFollowing),
                        OtherFollowing = criends.Count(p => p.IsFollowing),
                        OtherFollowers = criends.Count(p => p.IsFollower)
                    };
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler(ex);
            }
        }

        public async Task<CriendsRelationsBo> GetCriendRelation(Guid my, Guid criend)
        {
            try
            {
                var res = this.uow.Context.UserRelations.FirstOrDefault(p => p.OwnerId == my && p.UserId == criend);
                if (res is null)
                {
                    return new CriendsRelationsBo
                    {
                        IsBlock = false,
                        IsFollower = false,
                        IsFollowing = false
                    };
                }
                else
                {
                    return Mapper.Map<CriendsRelationsBo>(res);
                }
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public async Task<List<SearchCriendsResultBo>> Search(List<Guid> userids, Guid ownerId, IDbConnection cn = null)
        {
            try
            {
                if (cn == null)
                {
                    cn = DatabaseInfo.Connection;
                }
                var sql = new StringBuilder();
                sql.Append("select UserId,Email,Name,ProfileImage from [User]    WHERE  UserId in @UserId ");
                var r = cn.Query<SearchCriendsResultBo>(sql.ToString(), new
                {
                    UserId = userids,
                }).ToList();
                var query = @"SELECT UserId,IsFollowing,IsFollower,IsBlock from UserRelations where UserId in @UserId
                        AND OwnerId =  @OwnerId; ";
                var relations = cn.Query<CriendsRelationsBo>(query
                        , new
                        {
                            UserId = userids,
                            OwnerId = ownerId
                        }).ToList();
                foreach (var obj in r)
                {
                    obj.ProfileImage = $"{Bo.Utility.Configs.ImagePrefixBlob}{Bo.Enums.Enums.Imagetype.profileimages}/{obj.ProfileImage}";
                    obj.Relationships = relations.FirstOrDefault(p => p.UserId == obj.UserId) ?? new CriendsRelationsBo();
                }
                return r;
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
    }
}
