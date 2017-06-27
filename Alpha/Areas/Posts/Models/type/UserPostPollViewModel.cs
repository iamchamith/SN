using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alpha.Areas.Posts.Models
{
    public class UserPostPollViewModel : PostViewModel
    {
        public string Vs1Data { get; set; }
        public string Vs2Data { get; set; }
        public string Vs1Url { get; set; }
        public string Vs2Url { get; set; }
    }
}
