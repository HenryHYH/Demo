using WebApp.Entities;

namespace WebApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User Get(long id)
        {
            return new User
            {
                Id = id,
                Name = $"[{id}]"
            };
        }
    }
}
