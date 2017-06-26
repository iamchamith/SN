using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo
{
    public class SearchCriendsResultBo
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string Name { get; set; }
        public List<UserTagBo> UserTags { get; set; }
        public CriendsRelationsBo Relationships { get; set; }
        public SearchCriendsResultBo()
        {
            UserTags = new List<UserTagBo>();
            Relationships = new CriendsRelationsBo();
        }
    }
}
