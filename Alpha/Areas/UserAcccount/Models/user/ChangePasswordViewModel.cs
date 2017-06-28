using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Alpha.Areas.UserAcccount.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current password required")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "New password required")]
        public string NewPassword { get; set; }
    }
}