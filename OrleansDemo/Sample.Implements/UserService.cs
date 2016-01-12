using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Sample.Interfaces;

namespace Sample.Implements
{
    public class UserService : Grain, IUserService
    {
        public Task<bool> Exists(string mobile)
        {
            return Task.FromResult("10086" == mobile);
        }
    }
}
