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
    [Table("Tag")]
    public class Tag : IPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int Id { get; set; }
        [Required, StringLength(100), DataType("nvarchar")]
        public string TagName { get; set; }
        [Required, StringLength(1000), DataType("nvarchar")]
        public string Description { get; set; }
        [Required]
        public Guid Owner { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
