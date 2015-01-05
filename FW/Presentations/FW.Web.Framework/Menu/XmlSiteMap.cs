using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #region Utilities

        private void LoadSitMap(string path)
        {
            XElement element = XElement.Load(path);
            var list = element.Descendants();

            var doc = XDocument.Load(path);
            var nodes = doc.Nodes();
        }

        #endregion
    }
}
