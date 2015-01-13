namespace FW.Web.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class PageBulider : IPageBulider
    {
        #region Fields

        private IList<Resource> resources;

        #endregion Fields

        #region Constructors

        public PageBulider()
        {
            resources = new List<Resource>();
        }

        #endregion Constructors

        #region Methods

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

        public ResourceLocation GetResourceLocation(string path)
        {
            if (GetResourceType(path) == ResourceType.Script)
                return ResourceLocation.Footer;
            else
                return ResourceLocation.Head;
        }

        public ResourceType GetResourceType(string path)
        {
            if (path.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase))
                return ResourceType.Script;
            else if (path.EndsWith(".css", StringComparison.InvariantCultureIgnoreCase))
                return ResourceType.StyleSheet;

            return ResourceType.Null;
        }

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

        #endregion Methods

        #region Nested Types

        private class Resource
        {
            #region Properties

            public ResourceLocation Location
            {
                get; set;
            }

            public string Path
            {
                get; set;
            }

            public ResourcePriority Priority
            {
                get; set;
            }

            public ResourceType Type
            {
                get; set;
            }

            #endregion Properties
        }

        #endregion Nested Types
    }
}