using System;

namespace ExportDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start");
            ExcelHelper.Export();
            Console.WriteLine("Finish");
            Console.ReadLine();
        }
    }
}
