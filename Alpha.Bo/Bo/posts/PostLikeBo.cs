using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo
{
    public class PostLikeBo
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public bool IsLike { get; set; }
        public bool IsDisLike { get; set; }
    }
}
