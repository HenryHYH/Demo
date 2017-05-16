using ConsoleApp.Models;
using ConsoleApp.Validate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // http://www.technofattie.com/2011/10/05/recursive-validation-using-dataannotations.html

            var user = new User
            {
                Name = "Hello world",
                Address = new Address
                {
                    City = "GZ",
                    State = "GD",
                    Zip = new ZipCode
                    {
                    }
                }
            };

            var context = new ValidationContext(user, null, null);
            IList<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(user, context, results, true);

            Print(results, 0);

            Console.ReadKey();
        }

        private static void Print(IEnumerable<ValidationResult> results, int indentationLevel)
        {
            foreach (var item in results)
            {
                SetIdentation(indentationLevel);

                Console.WriteLine(item.ErrorMessage);

                if (item is CompositeValidationResult)
                    Print(((CompositeValidationResult)item).Results, indentationLevel + 1);
            }
        }

        private static void SetIdentation(int indentationLevel)
        {
            Console.CursorLeft = indentationLevel * 4;
        }
    }
}
