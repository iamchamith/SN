using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Areas.Posts.Models
{
    public class DoPostLIkeViewModel
    {
        public string PostId { get; set; }
        public PostLikeType Type { get; set; }
        public bool IsSelect { get; set; }
        public PostLikeModeType PostLikeModeType { get; set; }
    }
}