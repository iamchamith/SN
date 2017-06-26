using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class EmailModel
    {
        public string ToPrimary { get; set; }
        public List<string> ToSecondary { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public EmailModel()
        {
            ToSecondary = new List<string>();
        }
    }
}