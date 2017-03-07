using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ICaching cache = new EntlibCaching();
            var dt = cache.Get("A", () =>
            {
                return DateTime.Now;
            });

            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.ffff}", dt);

            Console.WriteLine("等待一秒");
            System.Threading.Thread.Sleep(1000);
            var dt2 = cache.Get<DateTime>("A");
            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.ffff}", dt2);

            Console.ReadKey();
        }
    }
}
