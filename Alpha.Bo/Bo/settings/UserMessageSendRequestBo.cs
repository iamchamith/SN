using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo.settings
{
    public class UserMessageSendRequestBo:PaginBo
    {
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
    }
}
