using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        [EmailAddress(ErrorMessage = "Invalied email address"), Required(ErrorMessage = "Email required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password required"), MinLengthAttribute(3)]
        public string Password { get; set; }
        public DateTime Dob { get; set; }
        public DateTime RegDate { get; set; }
    }
}