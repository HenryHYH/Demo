using System;
using System.Collections.Generic;
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
            using (var host = new SiloHost("Default"))
            {
                host.LoadOrleansConfig();
                host.InitializeOrleansSilo();
                host.StartOrleansSilo();

                Console.WriteLine("Press any key to exit.");
                Console.ReadKey(true);

                host.StopOrleansSilo();
            }
        }
    }
}
