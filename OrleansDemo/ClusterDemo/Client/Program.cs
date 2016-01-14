using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Sample.Interfaces;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GrainClient.Initialize();

            while (true)
            {
                Console.Write("输入用户名：");
                var name = Console.ReadLine();
                var userService = GrainClient.GrainFactory.GetGrain<IUserGrain>(name);
                Console.WriteLine("{0}{1}", name, userService.Exists().Result ? "已存在" : "不存在");

                if ("exit" == name)
                {
                    GrainClient.Uninitialize();
                    break;
                }
            }
        }
    }
}
