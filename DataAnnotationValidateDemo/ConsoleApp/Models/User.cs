using ConsoleApp.Validate;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }

        [Required, ValidateObject]
        public Address Address { get; set; }
    }
}
