namespace FW.Web.Framework.UI.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;

    public class XmlSiteMap
    {
        #region Constructors

        public XmlSiteMap(string path)
        {
            LoadSitMap(path);
        }

        #endregion Constructors

        #region Properties

        public SiteMapNode Node
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        private SiteMapNode GetNode(XElement element)
        {
            SiteMapNode node = new SiteMapNode();

            if (element.HasAttributes)
                foreach (var attr in element.Attributes())
                    node.Attributes.Add(attr.Name.LocalName.ToLower(), attr.Value);

            if (element.HasElements)
                foreach (var item in element.Elements())
                    node.Children.Add(GetNode(item));

            return node;
        }

        private void LoadSitMap(string path)
        {
            Node = GetNode(XElement.Load(path).Elements().FirstOrDefault());
        }

        #endregion Methods
    }
}