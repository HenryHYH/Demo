using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model
{
    public class CustomerAttribute
    {
        public int Id { get; set; }

        public DateTime CTime { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public virtual Customer AssociationCustomer { get; set; }
    }
}
