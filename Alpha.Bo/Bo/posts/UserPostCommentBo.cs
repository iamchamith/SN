using System;

namespace Alpha.Bo { 
 
    public class UserPostCommentBo : IBo
    {
        public int Id { get; set; }
        public int UserPostId { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
