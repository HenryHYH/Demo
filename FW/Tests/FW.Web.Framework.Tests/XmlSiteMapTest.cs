namespace FW.Web.Framework.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Web.Framework.UI.Menu;

    using Xunit;

    public class XmlSiteMapTest
    {
        #region Methods

        [Fact]
        public void TestLoad()
        {
            XmlSiteMap siteMap = new XmlSiteMap(AppDomain.CurrentDomain.BaseDirectory + "/TestData/Web.sitemap");
        }

        #endregion Methods
    }
}