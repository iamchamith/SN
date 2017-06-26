using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Alpha.Poco
{
    [Table("UserPostComment")]
    public class UserPostComment : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public Guid UserPostId { get; set; }
        [Required, DataType("nvarchar"), StringLength(500)]
        public string Comment { get; set; }
        [Required,ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CommentDate { get; set; }

        public UserPostComment()
        {
            this.CommentDate = DateTime.UtcNow;
        }
    }
}
