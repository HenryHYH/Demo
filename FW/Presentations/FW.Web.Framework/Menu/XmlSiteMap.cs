using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FW.Web.Framework.Menu
{
    public class XmlSiteMap
    {
        #region Ctor

        public XmlSiteMap(string path)
        {
            LoadSitMap(path);
        }

        #endregion

        #region Properties

        public SiteMapNode Node { get; private set; }

        #endregion

        #region Utilities

        private void LoadSitMap(string path)
        {
            Node = GetNode(XElement.Load(path).Elements().FirstOrDefault());
        }

        private SiteMapNode GetNode(XElement element)
        {
            SiteMapNode node = new SiteMapNode();

            if (element.HasAttributes)
                foreach (var attr in element.Attributes())
                    node.Attributes.Add(attr.Name.LocalName, attr.Value);

            if (element.HasElements)
                foreach (var item in element.Elements())
                    node.Children.Add(GetNode(item));

            return node;
        }

        #endregion
    }
}
