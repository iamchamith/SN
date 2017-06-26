using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Bo.Bo
{
    public class UserContactBo : IBo
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public SocialNetworks SocialNetwork { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool IsShowValue { get; set; }
        public UserContactBo()
        {
            IsShowValue = false;
        }
    }
}
