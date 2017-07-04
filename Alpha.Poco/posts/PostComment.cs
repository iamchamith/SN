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
    [Table("PostComment")]
    public class PostComment : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        [Required, DataType("nvarchar"), StringLength(500)]
        public string Comment { get; set; }
        [Required,ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CommentDate { get; set; }
        [Required, ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        public PostComment()
        {
            this.CommentDate = DateTime.UtcNow;
        }
    }
}
