using WebApp.Entities;

namespace WebApp.Repositories
{
    public interface IUserRepository
    {
        User Get(long id);
    }
}
