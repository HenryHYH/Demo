using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Domains;
using SqlSugar;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = DbHelper.GetInstance())
            {
                var query = db.Queryable<User>()
                              .Where(x => x.Enabled == true)
                              .Where(x => x.Age > 3);

                var sqlInfo = query.ToSql();
                Console.WriteLine("SQL = " + sqlInfo.Key);
                for (int i = 0, iMax = sqlInfo.Value.Length; i < iMax; i++)
                    Console.WriteLine("P" + i + " = " + sqlInfo.Value[i]);

                var list = query.ToList();
                Console.WriteLine("Count = " + list.Count);
            }

            Console.ReadKey();
        }
    }
}
