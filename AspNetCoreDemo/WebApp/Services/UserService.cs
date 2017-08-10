using WebApp.Entities;
using WebApp.Repositories;

namespace WebApp.Services
{
    public class UserService : IUserService
    {
        private IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public User Get(long id)
        {
            return repository.Get(id);
        }
    }
}
