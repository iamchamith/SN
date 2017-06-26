using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class TagInfoViewModel:UserTagsViewModel
    {
        public bool IsTagThere { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerProfileImage { get; set; }
        public Guid OwnerId { get; set; }
    }
}