using System;

namespace EntlibDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EntlibConfig.Configuration();
            Client.Run();

            Console.ReadKey();
        }
    }
}
