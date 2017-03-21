using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.Services
{
    public class Service : IService
    {
        public string GetA()
        {
            return "A";
        }

        public string GetB()
        {
            return "B";
        }

        public string GetC()
        {
            return "C";
        }
    }
}
