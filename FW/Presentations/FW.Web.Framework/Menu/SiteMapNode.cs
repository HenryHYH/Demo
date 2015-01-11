using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Web.Framework.Menu
{
    public class SiteMapNode
    {
        public SiteMapNode()
        {
            Children = new List<SiteMapNode>();
            Attributes = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Attributes { get; private set; }

        public string Title
        {
            get
            {
                return GetValue("title");
            }
        }

        public string Url
        {
            get
            {
                return GetValue("url");
            }
        }

        public string Description
        {
            get
            {
                return GetValue("description");
            }
        }

        public string Position
        {
            get
            {
                return GetValue("position");
            }
        }

        public IList<SiteMapNode> Children { get; private set; }

        private string GetValue(string key)
        {
            return Attributes.ContainsKey(key) ? Attributes[key] : string.Empty;
        }
    }
}
