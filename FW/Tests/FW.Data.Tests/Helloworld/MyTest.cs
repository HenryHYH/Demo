using FluentAssertions;
using FW.Core.Domain.Users;
using Xunit;

namespace FW.Data.Tests.Helloworld
{
    public class MyTest : PersistenceTest
    {
        [Fact]
        public void Test()
        {
            var user = new User()
            {
                Name = "Henry",
                Password = "mytest",
                Age = 1
            };

            var fromDb = SaveAndLoadEntity(user);
            fromDb.Should().NotBeNull();
            fromDb.Name.Should().Be("Henry");
        }

        [Fact]
        public void GetTest()
        {
            var user = context.Set<User>().Find(1);
            user.Name.Should().Be("Henry");
        }
    }
}
