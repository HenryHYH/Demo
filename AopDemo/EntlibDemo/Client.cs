using Model;

namespace EntlibDemo
{
    public class Client
    {
        public static void Run()
        {
            try
            {
                //EntlibConfig.Configuration();
                //IUserService userService = PolicyInjection.Create<UserService, IUserService>();

                EntlibConfig.Initialize();
                var userService = EntlibConfig.Resolve<IUserService>();

                userService.Register(User.Instance);
                userService.Register2(User.Instance);
            }
            catch
            {
                throw;
            }
        }
    }
}
