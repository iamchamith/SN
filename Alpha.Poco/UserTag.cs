using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alpha.Poco
{
    [Table("UserTag")]
    public class UserTag : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(Order = 0), Key, ForeignKey("Tag")]
        public int TagId { get; set; }
        [Column(Order = 1), Key, ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
