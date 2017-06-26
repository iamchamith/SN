using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class TagViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tag name required")]
        public string TagName { get; set; }
        public string Description { get; set; }
        public Guid Owner { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}