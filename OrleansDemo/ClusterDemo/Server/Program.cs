using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Runtime.Host;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OrleansConfiguration.xml"));

            Console.Write("输入节点名称：");
            var nodeName = Console.ReadLine() ?? string.Empty;
            using (var host = new SiloHost(nodeName, configFile))
            {
                host.LoadOrleansConfig();
                host.InitializeOrleansSilo();
                host.StartOrleansSilo();

                Console.WriteLine("Press any keys to exit.");
                Console.ReadKey();

                host.StopOrleansSilo();
            }
        }
    }
}
