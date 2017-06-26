using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class CriendProfilePreviewViewModel
    {
        public List<UserContactsViewModel> UserContacts { get; set; }
        public UserPreviewPageViewModel BasicInfo { get; set; }
        public List<UserTagsViewModel> UserTags { get;set; }
    }
}