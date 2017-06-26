using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Poco
{
    [Table("Errors")]
    public class Error
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string ExceptionMessage { get; set; }
        [DataType("nvarchar")]
        public string InputObject { get; set; }
        public string Stack { get; set; }
        public Guid User { get; set; }
        public DateTime Date { get; set; }
        [StringLength(200)]
        public string Module { get; set; }
    }
}
