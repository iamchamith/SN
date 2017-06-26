using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo
{
    public class CriendsRelationsBo
    {
        public bool IsFollowing { get; set; }
        public bool IsFollower { get; set; }
        public bool IsBlock { get; set; }
        public Guid UserId { get; set; }
    }
}
