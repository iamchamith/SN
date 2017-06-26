using Alpha.Bo.Bo;
using Alpha.Bo.Enums;
using Alpha.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Service.Interfaces
{
    public interface IUserContact : IRepository<UserContactBo, int>
    {
        Task Delete(SocialNetworks id, Guid userid);
        IEnumerable<DropDownBo> Read(bool enums = true);
        Task<UserContactBo> Read(Enums.SocialNetworks id, Guid userid);
        Task<List<UserContactBo>> Read(Guid userid);
    }
}
