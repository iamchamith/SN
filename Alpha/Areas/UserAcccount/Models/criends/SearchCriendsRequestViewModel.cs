using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class SearchCriendsRequestViewModel
    {
        public string Name { get; set; }
        public int Country { get; set; }
        public int Sex { get; set; }
        public int MaritalStatus { get; set; }
        public List<int> Tags { get; set; }
        public Guid OwnerId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public SearchCriendsRequestViewModel()
        {
            this.Tags = new List<int>();
        }
    }
}