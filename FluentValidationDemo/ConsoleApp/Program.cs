using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new Model()
            {
                Name = "中",
                Age = 100
            };
            var validator = new ModelValidator();
            var result = validator.Validate(model);

            Console.WriteLine(result.IsValid);
            foreach (var item in result.Errors)
            {
                Console.WriteLine("ErrorMessage = " + item.ErrorMessage);
                Console.WriteLine("Property = " + item.PropertyName);
            }

            var str = "中";

            Console.WriteLine(Encoding.Default.GetByteCount(str));
            Console.WriteLine(Encoding.Default.GetBytes(str).Count());

            Console.ReadKey();
        }
    }
}
