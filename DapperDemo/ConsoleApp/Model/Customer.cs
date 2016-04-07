using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model
{
    public class Customer
    {
        public Customer()
        {
            Attributes = new List<CustomerAttribute>();
        }

        public int Id { get; set; }

        public DateTime CTime { get; set; }

        public string Name { get; set; }

        public IList<CustomerAttribute> Attributes { get; set; }
    }
}
