using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.Infrastructure
{
    public class WebHelper : IWebHelper
    {
        public bool Post(string url, string[] name, string[] value, out string message)
        {
            message = "Impl";

            return true;
        }
    }
}
