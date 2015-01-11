namespace FW.Service.Users
{
    using System;
    using System.Linq;

    using FW.Core;
    using FW.Core.Data;
    using FW.Core.Domain.Users;

    public class UserService : IUserService
    {
        #region Fields

        private readonly IRepository<User> userRepository;

        #endregion Fields

        #region Constructors

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        #endregion Constructors

        #region Methods

        public void DeleteUser(User user)
        {
            userRepository.Delete(user);
        }

        public User GetUserById(int id)
        {
            return userRepository.Table.Where(x => x.Id == id).FirstOrDefault();
        }

        public User GetUserByRealName(string realName)
        {
            return userRepository.Table
                .Where(x => x.RealName == realName)
                .FirstOrDefault();
        }

        public IPagedList<User> GetUsers(DateTime? createdFrom = null,
            DateTime? createdTo = null,
            int pageIndex = 0,
            int pageSize = 50)
        {
            var query = userRepository.Table;

            if (createdFrom.HasValue)
            {
            }
            if (createdTo.HasValue)
            {
            }

            return new PagedList<User>(query, pageIndex, pageSize);
        }

        public void InsertUser(User user)
        {
            userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }

        #endregion Methods
    }
}