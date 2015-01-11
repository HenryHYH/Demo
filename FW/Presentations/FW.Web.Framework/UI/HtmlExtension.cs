namespace FW.Web.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using FW.Core.Infrastructure;

    public static class HtmlExtension
    {
        #region Methods

        public static void AddBreadCrumb(this HtmlHelper htmlHelper, string title, string path)
        {
            GetPageBulider().AddBreadCrumb(title, path);
        }

        public static void AddResource(this HtmlHelper htmlHelper, string path, ResourcePriority priority = ResourcePriority.Normal)
        {
            var pageBulider = GetPageBulider();
            AddResource(htmlHelper, path, pageBulider.GetResourceType(path), priority, pageBulider.GetResourceLocation(path));
        }

        public static void AddResource(this HtmlHelper htmlHelper, string path, ResourceType type, ResourcePriority priority = ResourcePriority.Normal, ResourceLocation location = ResourceLocation.Head)
        {
            GetPageBulider().AddResource(path, priority, type, location);
        }

        public static MvcHtmlString GenerateResources(this HtmlHelper htmlHelper, UrlHelper urlHelper, ResourceLocation location)
        {
            return MvcHtmlString.Create(GetPageBulider().GenerateResourcesPath(urlHelper, location));
        }

        private static IPageBulider GetPageBulider()
        {
            return EngineContext.Current.Resolve<IPageBulider>();
        }

        #endregion Methods
    }
}