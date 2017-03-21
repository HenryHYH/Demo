using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Request
    {
        public Request()
        {
            A = "Request - A";
            B = "Request - B";
        }

        public string A { get; set; }

        public string B { get; set; }
    }
}
