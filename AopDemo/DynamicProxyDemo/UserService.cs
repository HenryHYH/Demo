using Model;
using System;

namespace DynamicProxyDemo
{
    public interface IUserService
    {
        void Register(User user);
    }

    public class UserService : IUserService
    {
        public virtual void Register(User user)
        {
            Console.WriteLine("Register. {0}", user.ToString());
        }
    }
}
