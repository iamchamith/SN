using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class UserMessageViewModel
    {
        public int Id { get; set; }
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
        [Required, StringLength(500)]
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
    }
}