using SDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SDK.Tests
{
    public class DicFuncTest
    {
        [Fact]
        public void Test()
        {
            IService service = new Service();

            IDictionary<string, Func<string>> dt = new Dictionary<string, Func<string>>()
            {
                { "A", service.GetA },
                { "B", service.GetB },
                { "C", service.GetC },
                { "D", GetD }
            };

            Assert.Equal("A", dt["A"]());
            Assert.Equal("D", dt["D"]());
        }

        private string GetD()
        {
            return "D";
        }
    }
}
