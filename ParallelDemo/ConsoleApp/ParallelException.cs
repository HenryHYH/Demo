using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ParallelException
    {
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        private void Run1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Run1");
            throw new Exception("Run1 Exception");
        }

        private void Run2()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Run2");
            throw new Exception("Run2 Exception");
        }

        public void Test()
        {
            sw.Start();
            try
            {
                Parallel.Invoke(Run1, Run2);
            }
            catch (AggregateException aex)
            {
                foreach (var ex in aex.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            sw.Stop();
            Console.WriteLine("Parallel run " + sw.ElapsedMilliseconds + " ms.");

            sw.Reset();
            sw.Start();
            try
            {
                Run1();
                Run2();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sw.Stop();
            Console.WriteLine("Normal run " + sw.ElapsedMilliseconds + " ms.");
        }
    }
}
