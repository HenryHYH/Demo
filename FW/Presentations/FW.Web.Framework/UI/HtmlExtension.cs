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
        public static void AppendResource(this HtmlHelper htmlHelper, string path)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            AppendResource(htmlHelper, path, pageBulider.GetResourceType(path), pageBulider.GetResourceLocation(path));
        }

        public static void AppendResource(this HtmlHelper htmlHelper, string path, ResourceLocation location)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            AppendResource(htmlHelper, path, pageBulider.GetResourceType(path), location);
        }

        public static void AppendResource(this HtmlHelper htmlHelper, string path, ResourceType type, ResourceLocation location = ResourceLocation.Head)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            pageBulider.AppendResource(path, type, location);
        }

        public static void AddResource(this HtmlHelper htmlHelper, string path)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            AddResource(htmlHelper, path, pageBulider.GetResourceType(path), pageBulider.GetResourceLocation(path));
        }

        public static void AddResource(this HtmlHelper htmlHelper, string path, ResourceLocation location)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            AddResource(htmlHelper, path, pageBulider.GetResourceType(path), location);
        }

        public static void AddResource(this HtmlHelper htmlHelper, string path, ResourceType type, ResourceLocation location = ResourceLocation.Head)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            pageBulider.AddResource(path, type, location);
        }

        public static MvcHtmlString GenerateResources(this HtmlHelper htmlHelper, UrlHelper urlHelper, ResourceLocation location)
        {
            var pageBulider = EngineContext.Current.Resolve<IPageBulider>();
            return MvcHtmlString.Create(pageBulider.GenerateResourcesPath(urlHelper, location));
        }
    }
}
