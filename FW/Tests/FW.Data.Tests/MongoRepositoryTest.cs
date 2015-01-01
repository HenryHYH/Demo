using FluentAssertions;
using FW.Core.Data;
using FW.Core.Domain.Users;
using FW.Data;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace FW.Core.Tests
{
    public class MongoRepositoryTest
    {
        private IRepository<User> userRepository;

        public MongoRepositoryTest()
        {
            userRepository = new MongoRepository<User>();
        }

        [Theory]
        [InlineData("Henry", "123")]
        [InlineData("Hello world", "1234")]
        public void InsertTest(string realName, string password)
        {
            userRepository.Insert(new User()
            {
                RealName = realName,
                Password = password
            });
        }

        [Fact]
        public void TableTest()
        {
            var list = userRepository.Table.ToList();
            list.Count().Should().Be(2);
        }
    }
}
