using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FW.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ViewEngines.Engines.Clear();
            var viewEngine = new CustomViewLocationRazorViewEngine();
            ViewEngines.Engines.Add(viewEngine);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }

    public class CustomViewLocationRazorViewEngine : RazorViewEngine
    {
        public CustomViewLocationRazorViewEngine()
            : base()
        {
            this.AreaViewLocationFormats
                = this.AreaMasterLocationFormats
                = this.AreaPartialViewLocationFormats
                = new[] { "~/{2}/Views/{1}/{0}.cshtml", "~/{2}/Views/Shared/{0}.cshtml" };

            this.ViewLocationFormats
                = this.MasterLocationFormats
                = this.PartialViewLocationFormats
                = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            this.FileExtensions = new[] { "cshtml" };
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return base.FileExists(controllerContext, virtualPath);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            string controllerName = controllerContext.RouteData.GetRequiredString("controller");

            IList<string> viewLocations = new List<string>();

            Array.ForEach(this.ViewLocationFormats, format => viewLocations.Add(string.Format(format, viewName, controllerName)));

            string areaName = GetAreaName(controllerContext.RouteData);
            if (!string.IsNullOrWhiteSpace(areaName))
            {
                Array.ForEach(this.AreaViewLocationFormats, format => viewLocations.Insert(0, string.Format(format, viewName, controllerName, areaName)));
            }

            foreach (string viewLocation in viewLocations)
            {
                if (File.Exists(controllerContext.HttpContext.Request.MapPath(viewLocation)))
                {
                    return new ViewEngineResult(this.CreateView(controllerContext, viewLocation, null), this);
                }
            }

            return new ViewEngineResult(viewLocations);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return FindView(controllerContext, partialViewName, null, useCache);
        }

        protected virtual string GetAreaName(RouteData routeData)
        {
            object obj2;
            if (routeData.DataTokens.TryGetValue("area", out obj2))
            {
                return (obj2 as string);
            }
            return GetAreaName(routeData.Route);
        }

        protected virtual string GetAreaName(RouteBase route)
        {
            var area = route as IRouteWithArea;
            if (area != null)
            {
                return area.Area;
            }
            var route2 = route as Route;
            if ((route2 != null) && (route2.DataTokens != null))
            {
                return (route2.DataTokens["area"] as string);
            }
            return null;
        }
    }
}
