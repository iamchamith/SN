using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Alpha.DbAccess
{
    public class DatabaseInfo
    {
        static string cns = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Alpha;Integrated Security=True;Pooling=False";
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
