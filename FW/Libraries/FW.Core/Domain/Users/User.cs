using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Core.Domain.Users
{
    public class User : BaseEntity
    {
        public string RealName { get; set; }

        public string Password { get; set; }
    }
}
