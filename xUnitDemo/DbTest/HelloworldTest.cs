using Xunit;

namespace DbTest
{
    public class HelloworldTest : BaseTest
    {
        [Fact]
        public void Can_Save_And_Load()
        {
            var obj = new Model
            {
                Name = "Hello world",
                Age = 100
            };

            var fromDb = SaveAndLoadEntity(obj);
            Assert.NotNull(fromDb);
            Assert.Equal("Hello world", fromDb.Name);
            Assert.Equal(100, fromDb.Age);
        }
    }
}
