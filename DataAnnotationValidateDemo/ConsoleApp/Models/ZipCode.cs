using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Models
{
    public class ZipCode
    {
        [Required]
        public string PrimaryCode { get; set; }

        public string SubCode { get; set; }
    }
}
