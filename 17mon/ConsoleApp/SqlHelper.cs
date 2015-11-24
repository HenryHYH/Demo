using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class SqlHelper
    {
        public static void Read()
        {
            string connectionString = "Data Source=192.168.111.10;User ID=sa;Password=8g3k5!w;";
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT IP FROM UserPacket.dbo.IP";
                    var command = new SqlCommand(sql);
                    command.Connection = conn;
                    using (var reader = command.ExecuteReader())
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                string ip = reader.GetString(0);
                                var msg = IP.Find(ip);
                                if (msg[0].Length > 4)
                                {
                                    Console.WriteLine(ip + " - " + string.Join(",", msg));
                                    // break;
                                }
                            }
                        }
                        catch { }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                catch
                { }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
