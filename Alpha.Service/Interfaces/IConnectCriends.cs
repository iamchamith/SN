﻿using Alpha.Bo.Bo;
using Alpha.Bo.Bo.criends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IConnectCriends
    {
        Task<List<SearchCriendsResultBo>> Search(SearchCriendsRequestBo item);

        Task AddRemoveRelation(CriendsRelationshipBo item);

        Task<RelationCountBo> GetCriendsRelationCount(Guid userid);

        Task<CriendsRelationsBo> GetCriendRelation(Guid my, Guid criend);
    }
}
