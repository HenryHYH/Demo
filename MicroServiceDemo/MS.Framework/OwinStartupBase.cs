using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;

namespace MS.Framework
{
    public class OwinStartupBase
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            HttpRoute(config);
            Filter(config);
            Formatter(config);
            ExtendConfig(config);

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

        private void Filter(HttpConfiguration config)
        {
            config.Filters.Add(new LoggingFilterAttribute());
        }

        private void Formatter(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
        }

        public virtual void ExtendConfig(HttpConfiguration config)
        {
        }
    }
}
