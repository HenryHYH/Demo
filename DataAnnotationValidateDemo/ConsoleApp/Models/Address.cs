using ConsoleApp.Validate;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Models
{
    public class Address
    {
        [Required]
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required, ValidateObject]
        public ZipCode Zip { get; set; }
    }
}
