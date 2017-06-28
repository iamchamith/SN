using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class UserPreferencesViewModel
    {
        public bool SendNotificationEmail { get; set; }
        public bool ShowAnonymas { get; set; }
        public bool ShowMyAsk { get; set; }
        public bool ShowMyContacts { get; set; }
        public bool ShowMyAnswers { get; set; }
    }
}