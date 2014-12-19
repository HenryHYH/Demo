using Model;
using Shuttle.Core.Infrastructure;
using Shuttle.ESB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = ServiceBus.Create().Start())
            {
                ColoredConsole.WriteLine(ConsoleColor.Magenta, "(to exit press enter on an empty line)");
                ColoredConsole.WriteLine(ConsoleColor.White, "Enter a message:");

                var message = Console.ReadLine();
                while (!string.IsNullOrWhiteSpace(message))
                {
                    bus.Send(new Notice()
                    {
                        Message = message
                    });

                    message = Console.ReadLine();
                }
            }
        }
    }
}
