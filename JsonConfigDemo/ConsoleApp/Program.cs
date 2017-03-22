using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonConfig;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // default.conf 属性 - 生成操作 - 嵌入的资源

            var storeOwner = Config.Default.StoreOwner;

            Console.WriteLine("StoreOwner = " + storeOwner);

            foreach (var fruit in Config.Default.Fruits)
                Console.WriteLine(fruit);

            Console.ReadKey();
        }
    }
}
