using Autofac.Integration.Mvc;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelloWeb.MessageSystem.WebApi
{
    /// <summary>
    /// Global
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary>
        /// App start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Start(object sender, EventArgs e)
        {
            var container = DependencyConfig.Register();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// PreSendRequestHeaders
        /// </summary>
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }
    }
}