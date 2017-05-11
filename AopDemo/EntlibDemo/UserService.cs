using Model;
using System;

namespace EntlibDemo
{
    public interface IUserService
    {
        [UserHandler]
        void Register(User user);

        void Register2(User user);
    }

    public class UserService : IUserService
    {
        public void Register(User user)
        {
            Console.WriteLine("Register. {0}", user.ToString());
        }

        public void Register2(User user)
        {
            Console.WriteLine("Register2. {0}", user);
        }
    }
}
