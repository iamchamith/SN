using Alpha.Bo;
using Alpha.Bo.Bo.posts;
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
        Task<Bo.UserPostInfoBo> Read(Guid postId, Guid userid);
        Task<List<UserPostSearchResponse>> SearchPost(UserPostSearchRequest request);
        Task<UserPostBo> Insert(PostQuestionBo item, UserPostBo userpostinfo);
        Task<UserPostBo> Insert(PostNeedCommentBo item, UserPostBo userpostinfo);
        Task<UserPostBo> Insert(PostPollBo item, UserPostBo userpostinfo);
    }
}
