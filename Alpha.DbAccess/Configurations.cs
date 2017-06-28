using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.DbAccess
{
    public class Configurations
    {
        public static string Conns = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Alpha;Integrated Security=True;Pooling=False";

        //public static string Conns = @"Server=tcp:criends.database.windows.net,1433;Initial Catalog=criends;Persist Security Info=False;User ID=iamchamith;Password=janson@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
