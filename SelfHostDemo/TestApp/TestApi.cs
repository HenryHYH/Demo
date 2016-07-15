using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using Flurl;
using Flurl.Http;

namespace TestApp
{
    public class TestApi
    {
        [Fact]
        public void TestUserGet()
        {
            var result = "http://127.0.0.1:8000"
                            .AppendPathSegment("API")
                            .AppendPathSegment("User")
                            .GetJsonListAsync()
                            .Result;

            Assert.Equal(4, result.Count());
        }
    }
}
