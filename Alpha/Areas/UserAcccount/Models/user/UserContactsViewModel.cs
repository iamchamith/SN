using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class UserContactsViewModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int SocialNetwork { get; set; }
        [Required(ErrorMessage = "Socian network type required")]
        public string Key { get; set; }
        [Required(ErrorMessage = "Socian network value required")]
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool IsShowValue { get; set; }
        public UserContactsViewModel()
        {
            IsShowValue = false;
        }
    }
}