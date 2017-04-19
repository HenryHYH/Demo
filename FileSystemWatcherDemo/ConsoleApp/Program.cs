using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // WatcherStrat(@"D:\", "*.*");
            var watcher = new FileWatcher(@"D:\", "*.*");

            Console.ReadKey();
        }
    }
}
