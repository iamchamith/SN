using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Bo.Bo
{
    public class PostLikeBo
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public PostLikeType PostLikeType { get; set; }
        public PostLikeModeType PostLikeModeType { get; set; }
        public bool IsAnonymas { get; set; }
    }
}
