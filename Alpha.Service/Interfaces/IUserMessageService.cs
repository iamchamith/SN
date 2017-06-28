using Alpha.Bo.Bo.settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IUserMessageService
    {
        List<UserMessageBo> Read(UserMessageSendRequestBo item);
        Task<UserMessageBo> Send(UserMessageBo item);
    }
}
