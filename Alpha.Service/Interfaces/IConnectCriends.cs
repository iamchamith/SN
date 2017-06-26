using Alpha.Bo.Bo;
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
    }
}
