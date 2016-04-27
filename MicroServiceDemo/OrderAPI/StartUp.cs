using System.Web.Http;
using MS.Framework;
using Owin;

namespace MS.OrderAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            HttpRoute(config);
            Filter(config);

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
    }
}
