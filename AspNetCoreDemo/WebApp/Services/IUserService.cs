using WebApp.Entities;

namespace WebApp.Services
{
    public interface IUserService
    {
        User Get(long id);
    }
}
