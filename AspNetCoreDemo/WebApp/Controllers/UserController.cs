using Microsoft.AspNetCore.Mvc;
using WebApp.Entities;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public string Index()
        {
            return userService.Get(1).Name;
        }

        public User Detail(long id)
        {
            return userService.Get(id);
        }
    }
}
