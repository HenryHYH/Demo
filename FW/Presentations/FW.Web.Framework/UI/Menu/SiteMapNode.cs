namespace FW.Web.Framework.UI.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SiteMapNode
    {
        #region Constructors

        public SiteMapNode()
        {
            Children = new List<SiteMapNode>();
            Attributes = new Dictionary<string, string>();
        }

        #endregion Constructors

        #region Properties

        public Dictionary<string, string> Attributes
        {
            get; private set;
        }

        public IList<SiteMapNode> Children
        {
            get; private set;
        }

        public string Description
        {
            get { return GetValue("description"); }
        }

        public string Icon
        {
            get { return GetValue("icon"); }
        }

        public string Position
        {
            get { return GetValue("position"); }
        }

        public string Title
        {
            get { return GetValue("title"); }
        }

        public string Url
        {
            get { return GetValue("url"); }
        }

        #endregion Properties

        #region Methods

        private string GetValue(string key)
        {
            return Attributes.ContainsKey(key) ? Attributes[key] : string.Empty;
        }

        #endregion Methods
    }
}