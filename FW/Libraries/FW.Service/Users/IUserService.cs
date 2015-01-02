using FW.Core;
using FW.Core.Domain.Users;
using System;

namespace FW.Service.Users
{
    public interface IUserService
    {
        IPagedList<User> GetUsers(DateTime? createdFrom = null,
            DateTime? createdTo = null,
            int pageIndex = 0,
            int pageSize = 50);

        User GetUserByRealName(string realName);

        User GetUserById(int id);

        void InsertUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);
    }
}
