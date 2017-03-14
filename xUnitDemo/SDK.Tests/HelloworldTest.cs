using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDK;
using Xunit;

namespace SDK.Tests
{
    public class HelloworldTest
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-1, 1, 0)]
        public void TestAdd(int a, int b, int expected)
        {
            var helloworld = new Helloworld();
            var result = helloworld.Add(a, b);

            Assert.Equal(expected, result);
        }
    }
}
