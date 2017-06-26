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
        [Required(ErrorMessage = "Titile requred")]
        public string Titile { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public bool IsAnonymas { get; set; }
    }
}