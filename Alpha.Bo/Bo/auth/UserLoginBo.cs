using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo
{
    public class UserLoginBo
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordBo : UserBo
    {
        public string NewPassword { get; set; }
    }
}
