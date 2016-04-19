using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace ProductAPI
{
    public class ProductService
    {
        #region Fields

        private IDisposable app;

        #endregion

        #region Ctor

        public ProductService()
        {
        }

        #endregion

        #region Methods

        public void Start()
        {
            app = WebApp.Start<Startup>("http://localhost:8802/");
        }

        public void Stop()
        {
            if (null != app)
                app.Dispose();
        }

        #endregion
    }
}
