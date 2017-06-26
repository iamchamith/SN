using Alpha.Areas.UserAcccount.Models.criends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class SearchCriendsResultViewModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string Name { get; set; }
        public List<UserTagsViewModel>  UserTags { get; set; }
        public CriendsRelationsViewModel Relationships { get; set; }
        public SearchCriendsResultViewModel()
        {
            UserTags = new List<UserTagsViewModel>();
            Relationships = new CriendsRelationsViewModel();
        }
    }
}