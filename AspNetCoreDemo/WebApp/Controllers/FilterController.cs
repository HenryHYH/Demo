using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [AddHeader("Author", "Henry")]
    public class FilterController : Controller
    {
        public string Index()
        {
            return "Hello world";
        }
    }
}
