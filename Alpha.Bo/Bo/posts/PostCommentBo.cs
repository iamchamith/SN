using System;

namespace Alpha.Bo { 

    public class PostCommentBo : IBo
    {
        public int Id { get; set; }
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        public DateTime CommentDate { get; set; }
        public bool IsAnonymas { get; set; }
    }
}
