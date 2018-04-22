using HelloWeb.MessageSystem.SDK;
using HelloWeb.MessageSystem.SDK.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HelloWeb.MessageSystem.Client.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IMessageSystem client = MessageSystemFactory.Create("http://localhost:57878/");
            var response = client.Log.PostByrequest(new LogModel()
            {
                ProjectName = "HelloWeb",
                AppName = "Client",
                LogLevelId = 10,
                ShortMessage = "Hello world",
                FullMessage = new Exception("Hello Exception", new Exception("Hello Inner")).ToString(),
                CreatedOnUtc = DateTime.UtcNow,
                Extended = new Dictionary<string, string>
                {
                    { "CTime", string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now) }
                }
            });

            return Content(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now));
        }
    }
}