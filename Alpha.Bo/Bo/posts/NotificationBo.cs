using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Bo.Bo.posts
{
    public class NotificationBo
    {
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
