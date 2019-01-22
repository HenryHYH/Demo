using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        public virtual IActionResult Get()
        {
            return new JsonResult(User.Claims.Select(x => new { x.Type, x.Value }));
        }
    }
}
