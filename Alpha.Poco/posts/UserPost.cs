using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Alpha.Bo.Enums.Enums;
using System.Collections.ObjectModel;

namespace Alpha.Poco
{
    [Table("UserPost")]
    public class UserPost : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key, Column(Order = 0),ForeignKey("User")]
        public Guid UserId { get; set; }
        [Key, Column(Order = 1), ForeignKey("Post")]
        public Guid PostId { get; set; }
        [Required]
        public YesNo IsPrimaryUser { get; set; }
        public DateTime PostDate { get; set; }
        public Guid ParentPostId { get; set; }
        [Required]
        public YesNo Anonymous { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int ResponseMinits { get; set; }
        public UserPost()
        {
            this.Likes = 0;
            this.Dislikes = 0;
            IsPrimaryUser = YesNo.Yes;
            Anonymous = YesNo.No;
            PostDate = DateTime.UtcNow;
        }
    }
}
