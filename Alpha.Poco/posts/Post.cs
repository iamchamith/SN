using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Alpha.Bo.Enums;

namespace Alpha.Poco
{
    [Table("Post")]
    public class Post : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public Guid PostId { get; set; }
        [Required]
        public Enums.PostType PostType { get; set; }
        [Required]
        public string Tags { get; set; }
        [Required]
        public string Topic { get; set; }
    }
}
