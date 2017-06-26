using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo
{
    public class CriendsRelationshipBo
    {
        public Guid OwnerId { get; set; }
        public Guid UserId { get; set; }
        public Enums.Enums.UserRelationshipStatus State { get; set; }
        public Enums.Enums.YesNo OparationType { get; set; }
    }
}
