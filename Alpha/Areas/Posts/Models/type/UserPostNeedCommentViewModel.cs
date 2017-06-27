using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Alpha.Areas.Posts.Models
{
    public class UserPostNeedCommentViewModel  : PostViewModel
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string AskCommentImage { get; set; }
    }
}
