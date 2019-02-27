namespace WebApp.Services
{
    public interface IUserService
    {
        string GetName();
    }

    public class UserService : IUserService
    {
        public string GetName()
        {
            return "Henry";
        }
    }

    public class WrappedUserService : IUserService
    {
        private readonly IUserService userService;

        public WrappedUserService(IUserService userService)
        {
            this.userService = userService;
        }

        public string GetName()
        {
            return $"Wrapped {userService.GetName()}";
        }
    }

    public class SelfService
    {
        public string GetMessage()
        {
            return "Hello myself";
        }
    }

    public interface IUserRepository { }

    public class UserRepository : IUserRepository { }

    public interface IProductRepository { }

    public class ProductRepository : IProductRepository { }
}
