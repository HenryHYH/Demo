using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Core.Domain.Users
{
    public class User : BaseEntity
    {
        public User()
        {
            this.CTime = DateTime.Now;
            this.UTime = DateTime.Now;
        }

        public DateTime CTime { get; set; }

        public DateTime UTime { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
