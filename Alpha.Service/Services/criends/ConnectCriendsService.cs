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
                sql.Append("select UserId,Email,Name from [User]    WHERE  UserId <> @UserId   AND   ");
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
                        obj.ProfileImage = ProfileImage(obj.Email);
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


    }
}
