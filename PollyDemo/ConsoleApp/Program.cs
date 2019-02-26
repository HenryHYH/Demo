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
                //Retry();
                CircuitBreaker();
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

        private static void CircuitBreaker()
        {
            var policy = Policy.Handle<TimeoutException>()
                            .CircuitBreaker(3, TimeSpan.FromMinutes(1));

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    policy.Execute(DoSomething);
                }
                catch (Polly.CircuitBreaker.BrokenCircuitException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("timeout");
                }
            }
        }

        private static int index = 0;

        private static int DoSomething()
        {
            Console.WriteLine($"DoSomething {index++}");
            throw new TimeoutException();
        }
    }
}
