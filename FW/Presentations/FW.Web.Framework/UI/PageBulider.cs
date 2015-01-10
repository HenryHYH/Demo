using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.Web.Framework.UI
{
    public class PageBulider : IPageBulider
    {
        #region Fields

        private Dictionary<ResourceType, Dictionary<ResourceLocation, IList<string>>> resources;

        #endregion

        #region Ctor

        public PageBulider()
        {
            resources = new Dictionary<ResourceType, Dictionary<ResourceLocation, IList<string>>>();
        }

        #endregion

        #region Mehtods

        public string GenerateResourcesPath(UrlHelper urlHelper, ResourceLocation location)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var type in resources.Keys)
            {
                var typeResources = resources[type];
                if (typeResources.ContainsKey(location))
                {
                    var paths = resources[type][location];
                    foreach (var path in paths)
                    {
                        sb.Append(GenerateContent(type, urlHelper, path));
                        sb.Append(Environment.NewLine);
                    }
                }
            }

            return sb.ToString();
        }

        public void AddResource(string path, ResourceType type, ResourceLocation location)
        {
            if (!resources.ContainsKey(type))
            {
                resources.Add(type, new Dictionary<ResourceLocation, IList<string>>());
            }

            var typeResources = resources[type];
            if (!typeResources.ContainsKey(location))
            {
                typeResources.Add(location, new List<string>());
            }

            var locationResouces = typeResources[location];
            if (!locationResouces.Contains(path))
            {
                locationResouces.Add(path);
            }
        }

        public void AppendResource(string path, ResourceType type, ResourceLocation location)
        {
            if (!resources.ContainsKey(type))
            {
                resources.Add(type, new Dictionary<ResourceLocation, IList<string>>());
            }

            var typeResources = resources[type];
            if (!typeResources.ContainsKey(location))
            {
                typeResources.Add(location, new List<string>());
            }

            var locationResouces = typeResources[location];
            if (!locationResouces.Contains(path))
            {
                locationResouces.Insert(0, path);
            }
        }

        public ResourceType GetResourceType(string path)
        {
            if (path.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase))
                return ResourceType.Script;
            else if (path.EndsWith(".css", StringComparison.InvariantCultureIgnoreCase))
                return ResourceType.StyleSheet;

            return ResourceType.Null;
        }

        public ResourceLocation GetResourceLocation(string path)
        {
            if (GetResourceType(path) == ResourceType.Script)
                return ResourceLocation.Footer;
            else
                return ResourceLocation.Head;
        }

        #endregion

        #region Utilities

        private string GenerateContent(ResourceType type, UrlHelper urlHelper, string path)
        {
            string content = string.Empty;

            switch (type)
            {
                case ResourceType.Script:
                    content = string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", urlHelper.Content(path));
                    break;
                case ResourceType.StyleSheet:
                    content = string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\" />", urlHelper.Content(path));
                    break;
                case ResourceType.Null:
                default:
                    break;
            }

            return content;
        }

        #endregion
    }
}
