using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo
{
    public class TagInfoBo : UserTagBo, IBo
    {
        public bool IsTagThere { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerProfileImage { get; set; }
        public Guid OwnerId { get; set; }
    }
}
