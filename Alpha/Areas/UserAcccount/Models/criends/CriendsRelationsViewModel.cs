using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class CriendsRelationsViewModel
    {
        public bool IsFollowing { get; set; }
        public bool IsFollower { get; set; }
        public bool IsBlock { get; set; }
        public Guid UserId { get; set; }
    }
}