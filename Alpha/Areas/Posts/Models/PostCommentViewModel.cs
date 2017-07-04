using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Areas.Posts.Models
{
    public class PostCommentViewModel
    {
        public int Id { get; set; }
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        public DateTime CommentDate { get; set; }
    }
}