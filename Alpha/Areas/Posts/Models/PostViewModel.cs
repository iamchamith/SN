using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alpha.Areas.Posts.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public Guid PostId { get; set; }
        [Required(ErrorMessage = "Post topic required")]
        public string Topic { get; set; }
        public string Tags { get; set; }
        public bool IsAnonymas { get; set; }
    }
}