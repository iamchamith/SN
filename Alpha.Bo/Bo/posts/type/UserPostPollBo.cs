using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alpha.Bo.Bo.posts
{
    public class PostPollBo : PostBo, IBo
    {
        public string Vs1Url { get; set; }
        public string Vs2Url { get; set; }
    }
}
