using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Areas.UserAcccount.Models
{
    public class UserTagsViewModel
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public Guid UserId { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }
    }
}