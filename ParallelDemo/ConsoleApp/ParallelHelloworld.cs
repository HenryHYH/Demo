using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ParallelHelloworld
    {
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        private void Run1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Run 1");
        }

        private void Run2()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Run2");
        }

        public void Test()
        {
            sw.Start();
            Parallel.Invoke(Run1, Run2);
            sw.Stop();
            Console.WriteLine("Parallel run " + sw.Elapsed.TotalMilliseconds);

            sw.Restart();
            Run1();
            Run2();
            sw.Stop();
            Console.WriteLine("Normal run " + sw.Elapsed.TotalMilliseconds);
        }
    }
}
