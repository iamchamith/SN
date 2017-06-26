using Alpha.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IUserSettings: IRepository<UserBo,Guid>
    {
        Task<string> SendValidateEmailToken(Guid userid);
        Task ValidateEmailToken(string token, Guid userid);
        Task<UserBo> Read(Guid id);
    }
}
