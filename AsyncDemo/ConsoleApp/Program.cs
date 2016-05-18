using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始");
            // SyncMethod();
            AsyncMethod();
            Console.WriteLine("结束");

            Console.ReadKey();
        }

        #region 同步

        private static void SyncMethod()
        {
            Console.WriteLine("同步开始");

            int result = SyncWork(0);
            Console.WriteLine("Result = {0}", result);

            Console.WriteLine("同步结束");
        }

        private static int SyncWork(int val)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("第{0}次", (i + 1));
                Thread.Sleep(500);
                val++;
            }

            return val;
        }

        #endregion

        #region 异步

        private static async void AsyncMethod()
        {
            Console.WriteLine("同步开始");

            // int result = await AsyncWork(0);
            //int result = await Task.Factory.StartNew<int>(() =>
            //{
            //    return SyncWork(0);
            //});
            int result = await Task.Run<int>(() =>
            {
                return SyncWork(0);
            });

            Console.WriteLine("Result = {0}", result);

            Console.WriteLine("同步结束");
        }

        private static async Task<int> AsyncWork(int val)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("第{0}次", (i + 1));
                await Task.Delay(500);
                val++;
            }

            return val;
        }

        #endregion
    }
}
