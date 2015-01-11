namespace FW.Web.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using FW.Web.Framework.UI.BreadCrumb;

    public interface IPageBulider
    {
        #region Methods

        void AddBreadCrumb(string title, string url);

        void AddResource(string path, ResourcePriority priority, ResourceType type, ResourceLocation location);

        string GenerateResourcesPath(UrlHelper urlHelper, ResourceLocation location);

        IList<BreadCrumbNode> GetBreadCrumbs();

        ResourceLocation GetResourceLocation(string path);

        ResourceType GetResourceType(string path);

        #endregion Methods
    }
}