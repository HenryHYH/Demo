using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Sample.Interfaces;

namespace Sample.Implements
{
    public class UserGrain : Grain, IUserGrain
    {
        #region IUserGrain 成员

        public Task<bool> Exists()
        {
            Console.WriteLine("处理请求");
            Console.WriteLine(this.IdentityString);

            return Task.FromResult(this.GetPrimaryKeyString() == "Henry");
        }

        #endregion
    }
}
