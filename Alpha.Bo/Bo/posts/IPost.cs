using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo.posts
{
    public interface IPost
    {
        int Id { get; set; }
        Guid PostId { get; set; }
        string Topic { get; set; }
    }
}
