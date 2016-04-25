﻿using System.Web.Http;
using Owin;

namespace MS.ProductAPI
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
