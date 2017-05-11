using Model;

namespace StaticProxy
{
    public class Client
    {
        public static void Run()
        {
            try
            {
                IUserService userService = new UserServiceDecorator(new UserService());
                userService.Register(User.Instance);
            }
            catch
            {
                throw;
            }
        }
    }
}
