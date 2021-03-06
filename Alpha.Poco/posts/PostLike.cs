﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Poco
{
    [Table("PostLikes")]
    public class PostLike
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public Bo.Enums.Enums.PostLikeType PostLikeType { get; set; }
        public PostLikeModeType PostLikeModeType { get; set; }
        public Poll Poll { get; set; }
        public PostType PostType { get; set; }
        public bool IsAnonymas { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
