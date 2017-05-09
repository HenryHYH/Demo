using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyTest
{
    class Program
    {
        private static IList<TestTask> taskList = new List<TestTask>();
        private static int count = 0;
        private static DateTime startTime;
        private static int maxThread = 100;

        static void Main(string[] args)
        {
            //Test();
            //TaskTest();
            //ParallelTest();
        }

        private static void ParallelTest()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //serial implementation
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                //Do stuff
            }
            watch.Stop();
            Console.WriteLine("Serial Time: " + watch.ElapsedMilliseconds.ToString());

            //parallel implementation
            watch = new Stopwatch();
            watch.Start();
            System.Threading.Tasks.Parallel.For(0, 10, i =>
            {
                Thread.Sleep(1000);
                //Do stuff with i
            }
            );
            watch.Stop();
            Console.WriteLine("Parallel Time: " + watch.ElapsedMilliseconds.ToString());

            Console.WriteLine("Exit");
            Console.ReadLine();
        }

        private static void TaskTest()
        {
            Task t = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("I am the first task");
            });

            var t2 = t.ContinueWith(delegate
            {
                //simulate compute intensive
                Thread.Sleep(5000);
                return "Tasks Example";
            });

            //block1
            //string result = t2.Result;
            //Console.WriteLine("result of second task is: " + result);
            //end block1

            //block2
            t2.ContinueWith(delegate
            {
                Console.WriteLine("Here i am");
            });
            Console.WriteLine("Waiting my task");
            //end block2

            Console.WriteLine("Exit");
            Console.ReadLine();
        }

        private static void Test()
        {
            startTime = DateTime.Now;
            for (int i = 0; i < maxThread; i++)
                taskList.Add(new TestTask(i));

            var thread = new Thread(TaskFunction) { IsBackground = true };
            thread.Start();

            Console.ReadKey();
            thread.Abort();
        }

        private static void TaskFunction()
        {
            while (true)
            {
                foreach (var task in taskList)
                {
                    Task.Run(() =>
                    {
                        var time = task.ReceiveData();
                        Interlocked.Increment(ref count);
                        if (-1 != time)
                            ShowInfo(task.TaskId, time);
                    });
                }
                Thread.Sleep(50);
            }
        }

        private static void ShowInfo(int taskId, int time)
        {
            var ts = DateTime.Now - startTime;
            var cps = count / ts.TotalSeconds;

            // Console.Clear();
            Console.WriteLine($"[共运行{ts.TotalSeconds}秒，每秒接收{cps}次] 线程{taskId}收到{time}!");
        }
    }
}
