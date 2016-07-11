using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ParallelBreak
    {
        public void Test()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Parallel.For(0, 1000, (i, state) =>
            {
                if (bag.Count == 300)
                {
                    // state.Stop();
                    state.Break();
                    return;
                }
                bag.Add(i);
            });
            sw.Stop();

            Console.WriteLine("Bag count is " + bag.Count + ", " + sw.Elapsed.TotalMilliseconds);
        }
    }
}
