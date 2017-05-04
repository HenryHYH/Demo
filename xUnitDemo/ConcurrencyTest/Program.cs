using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyTest
{
    class Program
    {
        private static IList<TestTask> taskList = new List<TestTask>();
        private static int count = 0;
        private static DateTime startTime;
        private static int maxThread = 500;

        static void Main(string[] args)
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

            Console.WriteLine($"[共运行{ts.TotalSeconds}秒，每秒接收{cps}次] 线程{taskId}收到{time}!");
        }
    }
}
