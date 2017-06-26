using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Alpha.Bo.Exceptions;

namespace Alpha.Service.Services
{
    public class BaseService
    {
        public Exception ExceptionHandler(Exception ex)
        {
            ex = ex.GetBaseException();
            if (ex is SqlException)
            {
                var e = ex as SqlException;
                if (e.Number == 2627)
                {
                    return new PrimaryKeyViolationException("already exist");
                }
                else if (e.Number == 547)
                {
                    return new PrimaryKeyViolationException("fk excist");
                }
                return ex;
            }
            else
            {
                return ex;
            }
        }

        public string ProfileImage(string email) {
               return $"https://www.gravatar.com/avatar/{Alpha.Bo.Utility.Helper.MD5Hash(email)}";
        }
    }
}
