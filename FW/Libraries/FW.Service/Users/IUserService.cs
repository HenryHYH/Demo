using FW.Core;
using FW.Core.Domain.Users;
using System;

namespace FW.Service.Users
{
    public interface IUserService
    {
        IPagedList<User> GetUsers(DateTime? createdFrom, DateTime? createdTo);

        User GetUserByRealName(string realName);

        User GetUserById(int id);

        void InsertUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);
    }
}
