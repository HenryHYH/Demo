using HelloWeb.MessageSystem.SDK;
using HelloWeb.MessageSystem.SDK.Models;
using System;
using System.Web.Mvc;

namespace HelloWeb.MessageSystem.Client.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IHelloWebMessageSystem client = new HelloWebMessageSystem(new Uri("http://localhost:57878/"), new AnonymousCredential());
            var response = client.Log.PostByrequest(new LogModel()
            {
                ProjectName = "HelloWeb",
                AppName = "Client",
                Level = 10,
                Message = "Hello world",
                UniqueSequence = Guid.NewGuid().ToString(),
                CTime = DateTime.Now,
                Exception = new Exception("Hello Exception", new Exception("Hello Inner"))
            });

            return Content(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now));
        }
    }
}