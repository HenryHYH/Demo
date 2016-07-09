using Microsoft.Owin.Hosting;
using System;

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
            webapp = WebApp.Start<Startup>("http://localhost:8000");
        }

        public void Stop()
        {
            if (null != webapp)
                webapp.Dispose();
        }

        #endregion
    }
}
