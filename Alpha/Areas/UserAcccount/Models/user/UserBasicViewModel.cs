using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class UserBasicViewModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        [EmailAddress(ErrorMessage = "Invalied email address"), Required(ErrorMessage = "Email required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime Dob { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public int Gender { get; set; }
        public DateTime RegDate { get; set; }
        public int Country { get; set; }
        public int Language { get; set; }
        public int MaritalStatus { get; set; }
        public bool IsValiedEmail { get; set; }
        public string ProfileImage { get; set; }
        [StringLength(200)]
        public string Employeement { get; set; }
    }

    public class UserPreviewPageViewModel : UserBasicViewModel
    {
        public int Followings { get; set; }
        public int Followers { get; set; }
        public int FollowingsCriends { get; set; }
        public int FollowersCriends { get; set; }
        public int Asks { get; set; }
        public int Answers { get; set; }
        public new string Country { get; set; }
        public new string MaritalStatus { get; set; }
        public string[] Tags { get; set; }
        public new string Gender { get; set; }
        public bool IsMine { get; set; }
    }
}