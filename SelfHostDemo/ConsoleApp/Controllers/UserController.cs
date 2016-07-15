using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ConsoleApp.Core.Domain.Users;
using ConsoleApp.Models;
using ConsoleApp.Models.Users;
using ConsoleApp.Services.Users;

namespace ConsoleApp.Controllers
{
    public class UserController : ApiController
    {
        #region Fields

        private readonly IUserService userService;

        #endregion

        #region Ctor

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region Methods

        public IEnumerable<UserModel> Get()
        {
            return userService.Get()
                            .Select(x => x.Map<User, UserModel>())
                            .ToList();
        }

        public string Add([FromUri]User entity)
        {
            userService.Insert(entity);

            return "Success";
        }

        #endregion
    }
}
