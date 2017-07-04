using Alpha.Bo;
using Alpha.Bo.Bo.posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IPostCommentService : IRepository<PostCommentBo, Guid>
    {
        Task<List<PostCommentSearchResponse>> Search(Guid postid);
    }
}
