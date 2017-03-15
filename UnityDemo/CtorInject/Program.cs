using Microsoft.Practices.Unity;
using System;

namespace CtorInject
{
    class Program
    {
        static void Main(string[] args)
        {
            var helloworld = UnityConfig.Current.Resolve<Helloworld>();
            helloworld.Print();

            Console.ReadKey();
        }
    }
}
