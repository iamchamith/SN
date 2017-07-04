using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo.posts
{
    public class PostMyStatus
    {
        public Guid PostId { get; set; }
        public bool MeComment { get; set; }
        public bool MeLike { get; set; }
        public bool MeDislike { get; set; }
    }
}
