using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDK;
using Xunit;
using SDK.Infrastructure;
using Moq;

namespace SDK.Tests
{
    public class HelloworldTest
    {
        private readonly IWebHelper webHelper;

        public HelloworldTest()
        {
            webHelper = new WebHelper();
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-1, 1, 0)]
        public void TestAdd(int a, int b, int expected)
        {
            var helloworld = new Helloworld();
            var result = helloworld.Add(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestWebPost()
        {
            var helloworld = new Helloworld();

            Assert.Equal("Impl", helloworld.WebPost(webHelper));
        }

        [Fact]
        public void TestMockWebPost()
        {
            string mockMessage = "MockMessage1";
            var mock = new Mock<IWebHelper>();
            mock.Setup<bool>(x => x.Post(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string[]>(), out mockMessage))
                .Returns(false);

            var helloworld = new Helloworld();

            Assert.Equal("MockMessage1", helloworld.WebPost(mock.Object));
        }
    }
}
