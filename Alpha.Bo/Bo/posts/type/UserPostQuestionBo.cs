using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Bo.Bo.posts
{
    public class PostQuestionBo : PostBo, IBo
    {
        public string Description { get; set; }
    }
}
