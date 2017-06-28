using System;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Bo
{
    public class UserBo : IBo
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime Dob { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public Gender Gender { get; set; }
        public DateTime RegDate { get; set; }
        public int Country { get; set; }
        public int Language { get; set; }
        public int MaritalStatus { get; set; }
        public bool IsValiedEmail { get; set; }
        public string ProfileImage { get; set; }
        public string Employeement { get; set; }
    }
}
