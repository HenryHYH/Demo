using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new Model
            {
                Name = "中"
            };

            var result = new ModelValidator().Validate(obj);
            Console.WriteLine("IsValid = " + result.IsValid);
            foreach (var item in result.Errors)
            {
                Console.WriteLine(item.ErrorMessage);
            }

            Console.ReadKey();
        }
    }
}
