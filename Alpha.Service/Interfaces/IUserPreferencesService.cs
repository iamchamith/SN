using Alpha.Bo.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IUserPreferencesService:IRepository<UserPreferencesBo,int>
    {
        Task Update(List<UserPreferencesBo> item,Guid userid);
    }
}
