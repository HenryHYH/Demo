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

        private IList<Resource> resources;

        private class Resource
        {
            public ResourceType Type { get; set; }

            public ResourcePriority Priority { get; set; }

            public ResourceLocation Location { get; set; }

            public string Path { get; set; }
        }

        #endregion

        #region Ctor

        public PageBulider()
        {
            resources = new List<Resource>();
        }

        #endregion

        #region Mehtods

        public string GenerateResourcesPath(UrlHelper urlHelper, ResourceLocation location)
        {
            StringBuilder sb = new StringBuilder();

            var list = resources.Where(x => x.Location == location);
            var types = list.Select(x => x.Type).Distinct();
            foreach (var type in types)
            {
                var paths = list.Where(x => x.Type == type).OrderBy(x => x.Priority);
                foreach (var path in paths)
                {
                    sb.Append(GenerateContent(urlHelper, path.Type, path.Path));
                    sb.Append(Environment.NewLine);
                }
            }

            return sb.ToString();
        }

        public void AddResource(string path, ResourcePriority priority, ResourceType type, ResourceLocation location)
        {
            resources.Add(new Resource()
            {
                Path = path,
                Priority = priority,
                Type = type,
                Location = location
            });
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

        private string GenerateContent(UrlHelper urlHelper, ResourceType type, string path)
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
                case ResourceType.Raw:
                    content = path;
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
