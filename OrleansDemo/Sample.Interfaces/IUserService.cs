using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Sample.Interfaces
{
    public interface IUserService : IGrainWithIntegerKey
    {
        Task<bool> Exists(string mobile);
    }
}
