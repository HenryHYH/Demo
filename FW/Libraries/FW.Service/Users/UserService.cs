using FW.Core;
using FW.Core.Data;
using FW.Core.Domain.Users;
using System.Linq;

namespace FW.Service.Users
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IRepository<User> userRepository;

        #endregion

        #region Methods

        public User GetUserByRealName(string realName)
        {
            return userRepository.Table
                .Where(x => x.RealName == realName)
                .FirstOrDefault();
        }

        public IPagedList<User> GetUsers(System.DateTime? createdFrom, System.DateTime? createdTo)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void InsertUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new System.NotImplementedException();
        }

        #endregion


    }
}
