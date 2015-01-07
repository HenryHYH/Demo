using FW.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FW.Web.Framework.Tests
{
    public class XmlSiteMapTest
    {
        [Fact]
        public void TestLoad()
        {
            XmlSiteMap siteMap = new XmlSiteMap(AppDomain.CurrentDomain.BaseDirectory + "/TestData/Web.sitemap");
        }
    }
}
