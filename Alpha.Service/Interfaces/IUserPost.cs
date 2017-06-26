using Alpha.Bo;
using Alpha.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IUserPost : IRepository<UserPostInfoBo, Guid>
    {
        Task<UserPostInfoBo> Read(Guid postId, Guid userid);
        Task<List<UserPostSearchResponse>> SearchPost(UserPostSearchRequest request);
    }
}
