using Polly;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start");

            try
            {
                Retry();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("End");
            Console.ReadLine();
        }

        private static void Retry()
        {
            var policy = Policy.Handle<Exception>()
                            .WaitAndRetry(3, cnt => TimeSpan.FromSeconds(Math.Pow(2, cnt)), (err, countdown) =>
                            {
                                Console.WriteLine("Start retry");
                                Console.WriteLine(err.Message);
                                Console.WriteLine(countdown);
                                Console.WriteLine("End retry");
                            });

            policy.Execute(() =>
            {
                Console.WriteLine("Before execute");

                throw new Exception("Hello exeception");
            });
        }
    }
}
