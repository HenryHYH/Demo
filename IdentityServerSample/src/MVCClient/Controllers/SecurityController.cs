using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCClient.Controllers
{
    public class SecurityController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}