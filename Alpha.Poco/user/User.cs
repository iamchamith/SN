using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Alpha.Bo.Enums.Enums;
using System.ComponentModel;

namespace Alpha.Poco
{
    [Table("User")]
    public class User : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public Guid UserId { get; set; }
        [EmailAddress, Required, StringLength(50), Index("EmailIndex", IsUnique = true)]
        public string Email { get; set; }
        [Required, StringLength(500), DataType("nvarchar")]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        [StringLength(500)]
        public string Location { get; set; }
        [StringLength(5000), DataType("nvarchar")]
        public string Bio { get; set; }
        public Gender Gender { get; set; }
        public int Country { get; set; }
        public int Language { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsValiedEmail { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string Token { get; set; }
        public string ProfileImage { get; set; }
        [StringLength(200),DataType("nvarchar")]
        public string Employeement { get; set; }
        public bool IsStarter { get; set; }
        public User()
        {
            this.Gender = Gender.None;
            this.MaritalStatus = MaritalStatus.Not_selected;
            this.RegDate = DateTime.UtcNow;
        }
    }
}
