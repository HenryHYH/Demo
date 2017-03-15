using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtorInject
{
    public class Helloworld
    {
        private readonly IUserService userService;

        public Helloworld(IUserService userService)
        {
            this.userService = userService;
        }

        public void Print()
        {
            Console.WriteLine("Name = " + userService.GetName());
        }
    }
}
