using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Model;

namespace EntlibDemo
{
    public class Client
    {
        public static void Run()
        {
            try
            {
                IUserService userService = PolicyInjection.Create<UserService, IUserService>();
                userService.Register(User.Instance);
            }
            catch
            {
                throw;
            }
        }
    }
}
