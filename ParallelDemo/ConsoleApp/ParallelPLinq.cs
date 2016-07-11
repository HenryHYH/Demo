using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ParallelPLinq
    {
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private IList<Customer> list = new List<Customer>();

        public ParallelPLinq()
        {
            for (int i = 0; i < 2000000; i++)
            {
                list.Add(new Customer() { Name = "Jack", Age = 21 });
                list.Add(new Customer() { Name = "Jime", Age = 26 });
                list.Add(new Customer() { Name = "Tina", Age = 29 });
                list.Add(new Customer() { Name = "Luo", Age = 30 });
                list.Add(new Customer() { Name = "Wang", Age = 60 });
                list.Add(new Customer() { Name = "Feng", Age = 25 });
            }
        }

        public void Test()
        {
            sw.Start();
            var r1 = list.Where(x => x.Age >= 27).ToList();
            sw.Stop();
            Console.WriteLine("Linq = " + sw.Elapsed.TotalMilliseconds);

            sw.Restart();
            var r2 = list.AsParallel().Where(x => x.Age >= 27).ToList();
            sw.Stop();
            Console.WriteLine("PLinq = " + sw.Elapsed.TotalMilliseconds);
        }

        public void TestGroupBy()
        {
            sw.Start();
            var group1 = list.GroupBy(x => x.Age).ToList();
            foreach (var item in group1)
                Console.WriteLine("Age = {0}, Count = {1}", item.Key, item.Count());
            sw.Stop();
            Console.WriteLine("Linq = " + sw.Elapsed.TotalMilliseconds);

            sw.Restart();
            var group2 = list.ToLookup(x => x.Age);
            foreach (var item in group2)
                Console.WriteLine("Age = {0}, Count = {1}", item.Key, item.Count());
            sw.Stop();
            Console.WriteLine("PLinq = " + sw.Elapsed.TotalMilliseconds);
        }

        private class Customer
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }
    }
}
