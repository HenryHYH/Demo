using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // new ParallelHelloworld().Test();
            // new ParallelBreak().Test();
            // new ParallelException().Test();
            // new ParallelPLinq().Test();
            new ParallelPLinq().TestGroupBy();

            Console.ReadKey();
        }
    }
}
