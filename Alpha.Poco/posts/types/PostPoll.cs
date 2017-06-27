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
    [Table("PostPolls")]
    public class PostPoll : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public Guid PostId { get; set; }
        [Required, DataType("nvarchar"), StringLength(500)]
        public string Topic { get; set; }
        [Required,StringLength(500)]
        public string Vs1Url { get; set; }
        [Required, StringLength(500)]
        public string Vs2Url { get; set; }
    }
}
