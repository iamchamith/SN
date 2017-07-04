using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Service.Interfaces
{
    public interface IPostLikeService
    {
        Task LikeDislikePost(Guid userid,Guid postid, PostLikeType postLikeType, bool islike);
    }
}
