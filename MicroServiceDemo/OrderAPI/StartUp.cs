using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Owin;

namespace MS.OrderAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            HttpRoute(config);

            app.UseWebApi(config);
        }

        private void HttpRoute(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "{controller}/{id}",
                  defaults: new { id = RouteParameter.Optional }
              );
        }
    }
}
