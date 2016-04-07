using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class BaseTest : IDisposable
    {
        public static readonly string CONNECTION_STRING = "Data Source=192.168.111.20;Initial Catalog=Test;User ID=sa;Password=HWu7f8nH;";
        protected readonly IDbConnection conn;

        public BaseTest()
        {
            conn = new SqlConnection(CONNECTION_STRING);
            conn.Open();
        }

        public void Dispose()
        {
            if (null != conn && conn.State != ConnectionState.Closed)
                conn.Close();
        }
    }
}
