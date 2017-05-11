using Model;

namespace RealProxyDemo
{
    public class Client
    {
        public static void Run()
        {
            try
            {
                IUserService userService = TransparentProxy.Create<UserService>();
                userService.Register(User.Instance);
            }
            catch
            {
                throw;
            }
        }
    }
}
