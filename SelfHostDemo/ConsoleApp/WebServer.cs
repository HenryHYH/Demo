using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace ConsoleApp
{
    public class WebServer
    {
        #region Fields

        private IDisposable webapp;

        #endregion

        #region Methods

        public void Start()
        {
            var port = ConfigurationManager.AppSettings["PORT"];
            webapp = WebApp.Start<Startup>(string.Format("http://*:{0}", port));
        }

        public void Stop()
        {
            if (null != webapp)
                webapp.Dispose();
        }

        #endregion
    }
}
