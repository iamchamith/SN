using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo
{
    public class SessionBo:IBo
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Country { get; set; }
        public int Sex { get; set; }
        public int MaritalStatus { get; set; }
        public string ProfileImage { get; set; }
        public List<int> Tags { get; set; }
    }
}
