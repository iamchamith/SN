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
    [Table("Post")]
    public class Post : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public Guid PostId { get; set; }
        [Required, StringLength(500), DataType("nvarchar")]
        public string Titile { get; set; }
        [DataType("nvarchar")]
        public string Description { get; set; }
        [Required]
        public string Tags { get; set; }
        public Post()
        {
        }

    }
}
