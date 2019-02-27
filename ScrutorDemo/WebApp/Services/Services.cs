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

    public interface IUserRepository { }

    public class UserRepository : IUserRepository { }

    public interface IProductRepository { }

    public class ProductRepository : IProductRepository { }
}
