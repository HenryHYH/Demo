using System;
using Microsoft.Owin.Hosting;

namespace OrderAPI
{
    public class OrderService
    {
        #region Fields

        private IDisposable app;

        #endregion

        #region Ctor

        public OrderService()
        {
        }

        #endregion

        #region Methods

        public void Start()
        {
            app = WebApp.Start<Startup>("http://localhost:8801/");
        }

        public void Stop()
        {
            if (null != app)
                app.Dispose();
        }

        #endregion
    }
}
