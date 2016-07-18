using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Core.Caching;
using ConsoleApp.Core.Datas;
using ConsoleApp.Core.Domain.Users;

namespace ConsoleApp.Services.Users
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IRepository<User> repository;
        private readonly ICacheManager cacheManager;

        #endregion

        #region Ctor

        public UserService(IRepository<User> repository, ICacheManager cacheManager)
        {
            this.repository = repository;
            this.cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IEnumerable<User> Get()
        {
            var list = repository.Table
                                 .Where(x => x.Name.Contains("Test"));

            return list.ToList();
        }

        public void Insert(User entity)
        {
            repository.Add(entity);
        }

        #endregion
    }
}
