using Castle.DynamicProxy;
using Model;

namespace DynamicProxyDemo
{
    public class Client
    {
        public static void Run()
        {
            try
            {
                ProxyGenerator generator = new ProxyGenerator();
                Interceptor interceptor = new Interceptor();
                IUserService userService = generator.CreateClassProxy<UserService>(interceptor);
                userService.Register(User.Instance);
            }
            catch
            {
                throw;
            }
        }
    }
}
