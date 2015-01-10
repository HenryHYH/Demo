using FW.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.Web.Framework.UI
{
    public static class HtmlExtension
    {
        public static void AddResource(this HtmlHelper htmlHelper, string path, ResourcePriority priority = ResourcePriority.Normal)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            AddResource(htmlHelper, path, pageBulider.GetResourceType(path), priority, pageBulider.GetResourceLocation(path));
        }

        public static void AddResource(this HtmlHelper htmlHelper, string path, ResourceType type, ResourcePriority priority = ResourcePriority.Normal, ResourceLocation location = ResourceLocation.Head)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            pageBulider.AddResource(path, priority, type, location);
        }

        public static MvcHtmlString GenerateResources(this HtmlHelper htmlHelper, UrlHelper urlHelper, ResourceLocation location)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            return MvcHtmlString.Create(pageBulider.GenerateResourcesPath(urlHelper, location));
        }
    }
}
