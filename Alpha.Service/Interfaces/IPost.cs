using Alpha.Bo;
using Alpha.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IPost  :IRepository<PostBo,Guid>
    {
        Task<Guid> Insert(PostBo item);
    }
}
