using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ConsoleApp.Core.Domain.Users;
using ConsoleApp.Core.MessageQueues;
using ConsoleApp.Models;
using ConsoleApp.Models.Users;
using ConsoleApp.Services.Users;

namespace ConsoleApp.Controllers
{
    public class UserController : ApiController
    {
        #region Fields

        private readonly IUserService userService;
        private readonly IMessageQueueManager mqManager;

        #endregion

        #region Ctor

        public UserController(
            IUserService userService,
            IMessageQueueManager mqManager)
        {
            this.userService = userService;
            this.mqManager = mqManager;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            mqManager.Send("Hello world");

            return userService.Get()
                            .Select(x => x.Map<User, UserModel>())
                            .ToList();
        }

        [HttpGet]
        public string Test()
        {
            mqManager.Receive<string>(x =>
            {
                System.Console.WriteLine(x);
            });

            return "Test";
        }

        [HttpPost]
        public string Add([FromBody]UserModel model)
        {
            var entity = model.Map<UserModel, User>();
            userService.Insert(entity);

            return "Success";
        }

        #endregion
    }
}
