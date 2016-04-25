using System;
using Microsoft.Owin.Hosting;
using MS.Infrastructure;

namespace MS.OrderAPI
{
    public class StartupService
    {
        #region Fields

        private IDisposable app;
        private readonly ServerConfig config;

        #endregion

        #region Ctor

        public StartupService(ServerConfig config)
        {
            this.config = config;
        }

        #endregion

        #region Methods

        public void Start()
        {
            app = WebApp.Start<Startup>(config.Address.Url);
        }

        public void Stop()
        {
            if (null != app)
                app.Dispose();
        }

        #endregion
    }
}
