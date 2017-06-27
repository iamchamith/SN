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
    public class UserPostQuestionViewModel : PostViewModel
    {
        public string Description { get; set; }
    }
}
