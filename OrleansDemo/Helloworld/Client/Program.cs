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

            string mobile = "";
            do
            {
                Console.WriteLine("输入手机");
                mobile = Console.ReadLine();
                var userService = GrainClient.GrainFactory.GetGrain<IUserService>(0);
                Console.WriteLine("{0}{1}", mobile, (userService.Exists(mobile).Result ? "已存在" : "不存在"));
            }
            while (mobile != "quit");
        }
    }
}
