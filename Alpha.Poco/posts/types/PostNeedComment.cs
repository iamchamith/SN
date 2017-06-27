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
    [Table("PostNeedComments")]
    public class PostNeedComment : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public Guid PostId { get; set; }
        [Required, DataType("nvarchar"), StringLength(500)]
        public string Topic { get; set; }
        [Required, DataType("nvarchar"), StringLength(1500)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Tags { get; set; }
    }
}
