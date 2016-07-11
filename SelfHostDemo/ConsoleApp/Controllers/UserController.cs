using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ConsoleApp.Domain.Models;
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

        public IEnumerable<User> Get()
        {
            return userService.Get();
        }

        public string Add([FromUri]User entity)
        {
            userService.Insert(entity);

            return "Success";
        }

        #endregion
    }
}
