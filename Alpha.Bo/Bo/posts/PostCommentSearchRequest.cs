using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo.posts
{
    public class PostCommentSearchResponse 
    {
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public string CommentDateStr { get; set; }
        public bool IsMine { get; set; }
        public Guid PostId { get; set; }
    }
}
