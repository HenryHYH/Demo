using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace ConsoleApp
{
    public class DbHelper
    {
        public static readonly string ConnectionString = "Data Source=192.168.111.20;Initial Catalog=Test;User ID=sa;Password=HWu7f8nH;";

        public static SqlSugarClient GetInstance()
        {
            var db = new SqlSugarClient(ConnectionString);
            db.IsEnableLogEvent = true;
            db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par + "\r\n"); };

            return db;
        }
    }
}
