namespace FW.Service.Users
{
    using System;

    using FW.Core;
    using FW.Core.Domain.Users;

    public interface IUserService
    {
        #region Methods

        void DeleteUser(User user);

        User GetUserById(int id);

        User GetUserByRealName(string realName);

        IPagedList<User> GetUsers(DateTime? createdFrom = null,
            DateTime? createdTo = null,
            int pageIndex = 0,
            int pageSize = 50);

        void InsertUser(User user);

        void UpdateUser(User user);

        #endregion Methods
    }
}