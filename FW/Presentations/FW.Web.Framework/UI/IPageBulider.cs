using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.Web.Framework.UI
{
    public interface IPageBulider
    {
        string GenerateResourcesPath(UrlHelper urlHelper, ResourceLocation location);

        void AddResource(string path, ResourcePriority priority, ResourceType type, ResourceLocation location);

        ResourceType GetResourceType(string path);

        ResourceLocation GetResourceLocation(string path);
    }
}
