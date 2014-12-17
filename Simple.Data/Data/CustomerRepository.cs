using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;
using Model;

namespace Data
{
    public class CustomerRepository
    {
        public Customer Get(int id)
        {
            return (Customer)Database.Default.Customer.Get(id);
        }
    }
}
