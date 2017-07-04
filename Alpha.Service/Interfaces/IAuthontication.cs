using Alpha.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IAuthontication
    {
        SessionBo Login(UserLoginBo item);
        Task<SessionBo> Register(UserBo item);
        Task ChangePassword(ChangePasswordBo item);
        Task<string[]> ForgetPasswordRequest(string email);
        Task<SessionBo> ForgetPasswordRequestTokenValidate(string token, string email);
        Task ChangePassword(string password, Guid userid);
        Task StartCriends(Guid userid);
    }
}
