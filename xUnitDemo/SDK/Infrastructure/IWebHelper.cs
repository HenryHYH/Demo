using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.Infrastructure
{
    public interface IWebHelper
    {
        bool Post(string url, string[] name, string[] value, out string message);
    }
}
