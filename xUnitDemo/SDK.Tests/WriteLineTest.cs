using Xunit;
using Xunit.Abstractions;

namespace SDK.Tests
{
    public class WriteLineTest
    {
        private readonly ITestOutputHelper output;

        public WriteLineTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test()
        {
            output.WriteLine("Hello world");

            Assert.Equal(1, 1);
        }
    }
}
