using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Alpha.Poco
{
    [Table("UserMessages")]
    public class UserMessage : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
        [Required, StringLength(500), DataType("nvarchar")]
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set;  }
    }
}
