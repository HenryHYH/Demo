using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace WebApp.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ApiPermissionFilterAttribute : ActionFilterAttribute
    {
        private static readonly string HOST_PATTERN = "([^.]+\\.[^.]{1,3}(\\.[^.]{1,3})?)$";

        private string GetDomain(string host)
        {
            var match = Regex.Match(host, HOST_PATTERN);

            return match.Success ? match.Value : host;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var host = filterContext.HttpContext.Request.UrlReferrer.Host;
            var domain = GetDomain(host);

            if (!string.Equals("henry.com", domain, StringComparison.InvariantCultureIgnoreCase))
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

                return;
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var url = filterContext.HttpContext.Request.UrlReferrer.GetLeftPart(UriPartial.Authority);

            filterContext.HttpContext.Response.Headers.Remove("Access-Control-Allow-Origin");
            filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", url);

            filterContext.HttpContext.Response.Headers.Remove("Access-Control-Allow-Credentials");
            filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");

            filterContext.HttpContext.Response.Headers.Remove("Access-Control-Allow-Methods");
            filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");

            base.OnActionExecuted(filterContext);
        }
    }
}