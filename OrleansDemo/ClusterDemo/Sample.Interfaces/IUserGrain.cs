using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Sample.Interfaces
{
    public interface IUserGrain : IGrainWithStringKey
    {
        Task<bool> Exists();
    }
}
