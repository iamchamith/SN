using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo.settings
{
    public class UserMessageBo 
    {
        public int Id { get; set; }
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
    }
}
