using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo
{
    public class SearchCriendsRequestBo
    {
        public string Name { get; set; }
        public int Country { get; set; }
        public int Sex { get; set; }
        public int MaritalStatus { get; set; }
        public Guid OwnerId { get; set; }
        public List<int> Tags { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public SearchCriendsRequestBo()
        {
            this.Tags = new List<int>();
        }
    }
}
