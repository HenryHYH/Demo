using Model;
using System;

namespace EntlibDemo
{
    [UserHandler]
    public interface IUserService
    {
        void Register(User user);
    }

    public class UserService : IUserService
    {
        public void Register(User user)
        {
            Console.WriteLine("Register. {0}", user.ToString());
        }
    }
}
