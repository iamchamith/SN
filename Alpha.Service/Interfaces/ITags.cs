using Alpha.Bo;
using Alpha.Bo.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface ITags:IRepository<TagBo,int>
    {
        Task<List<DropDownBo>> Read(string q,Guid userid);
        Task Update(int tagid, bool isAdd);
    }
}
