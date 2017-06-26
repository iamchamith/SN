using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Poco
{
    [Table("UserRelations")]
    public class UserRelation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key, Column(Order = 0)]
        public Guid OwnerId { get; set; }
        [Key, Column(Order = 1)]
        public Guid UserId { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsFollower { get; set; }
        public bool IsBlock { get; set; }
    }
}
