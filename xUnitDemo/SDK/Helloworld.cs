using SDK.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{
    public class Helloworld
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public string WebPost(IWebHelper webHelper)
        {
            string message = "NULL";
            var result = webHelper.Post("", null, null, out message);

            return message;
        }
    }
}
