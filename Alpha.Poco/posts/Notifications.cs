using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Poco
{
    [Table("Notifications")]
    public class Notification : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
        public DateTime Datetime { get; set; }
        public string Description { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead { get; set; }
        public string Url { get; set; }
    }
}
