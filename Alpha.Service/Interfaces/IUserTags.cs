using Alpha.Bo;
using Alpha.Bo.Bo;
using System;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IUserTags : IRepository<UserTagBo, int>
    {
        Task<TagInfoBo> Read(Guid userid, int id);
    }
}
