using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Poco
{
    [Table("UserContacts")]
    public class UserContact : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public SocialNetworks SocialNetwork { get; set; }
        [Required, DataType("nvarchar"), StringLength(50)]
        public string Key { get; set; }
        [Required, DataType("nvarchar"), StringLength(500)]
        public string Url { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
