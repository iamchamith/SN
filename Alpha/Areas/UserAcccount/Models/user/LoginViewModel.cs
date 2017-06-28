using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Alpha.Areas.UserAcccount.Models
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Invalied email address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}