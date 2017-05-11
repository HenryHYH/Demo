using Model;
using System;

namespace StaticProxy
{
    public class UserServiceDecorator : IUserService
    {
        public IUserService UserService { get; private set; }

        public UserServiceDecorator(IUserService userService)
        {
            UserService = userService;
        }

        public void Register(User user)
        {
            Executing(user);
            UserService.Register(user);
            Executed(user);
        }

        private void Executing(User user)
        {
            Console.WriteLine("Executing");
        }

        private void Executed(User user)
        {
            Console.WriteLine("Executed");
        }
    }
}
