using System.Web.Http;

namespace OrderAPI.Controllers
{
    public class OrderController : ApiController
    {
        public string Get()
        {
            return "Order";
        }
    }
}
