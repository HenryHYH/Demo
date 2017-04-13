using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new Model() { A = "AA", B = "BB", C = 1 };
            var json = JsonConvert.SerializeObject(m);

            var dt = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);
            var str = string.Join("", dt.Values);

            Console.WriteLine(json);
            Console.WriteLine(str);

            Console.WriteLine(Enum.Test.ToString("D"));

            Console.ReadKey();
        }
    }
}
