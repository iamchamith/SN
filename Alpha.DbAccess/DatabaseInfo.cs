using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Alpha.DbAccess
{
    public class DatabaseInfo
    {
        static string cns = Bo.Utility.Configs.Conns;
        static SqlConnection cn = new SqlConnection();
        public static SqlConnection Connection
        {
            get
            {
                if (cn.State == System.Data.ConnectionState.Open)
                {
                    return cn;
                }
                else
                {
                    cn = new SqlConnection(cns);
                    cn.Open();
                    return cn;
                }
            }
        }
    }
}
