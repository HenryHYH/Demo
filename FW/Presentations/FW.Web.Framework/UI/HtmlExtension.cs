namespace FW.Web.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    using FW.Core.Infrastructure;

    public static class HtmlExtension
    {
        #region Methods

        public static void AddResource(this HtmlHelper htmlHelper, string path, ResourcePriority priority = ResourcePriority.Normal)
        {
            var pageBulider = GetPageBulider();
            AddResource(htmlHelper, path, pageBulider.GetResourceType(path), priority, pageBulider.GetResourceLocation(path));
        }

        public static void AddResource(this HtmlHelper htmlHelper, string path, ResourceType type, ResourcePriority priority = ResourcePriority.Normal, ResourceLocation location = ResourceLocation.Head)
        {
            GetPageBulider().AddResource(path, priority, type, location);
        }

        public static MvcHtmlString FWLabelFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            string labelText = GetDisplayName(htmlHelper, expression);

            return htmlHelper.LabelFor(expression, labelText, htmlAttributes);
        }

        public static MvcHtmlString GenerateResources(this HtmlHelper htmlHelper, UrlHelper urlHelper, ResourceLocation location)
        {
            return MvcHtmlString.Create(GetPageBulider().GenerateResourcesPath(urlHelper, location));
        }

        private static string GetDisplayName<TModel, TValue>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string displayName = string.Empty;
            object value = null;
            if (metadata.AdditionalValues.TryGetValue("ResourceDisplayName", out value))
            {
                var attr = value as ResourceDisplayNameAttribute;
                if (null != attr)
                {
                    displayName = attr.DisplayName;
                }
            }

            return displayName;
        }

        private static IPageBulider GetPageBulider()
        {
            return EngineContext.Current.Resolve<IPageBulider>();
        }

        #endregion Methods
    }
}