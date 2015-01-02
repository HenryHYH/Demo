using FluentAssertions;
using FW.Core.Data;
using FW.Core.Domain.Users;
using FW.Data;
using System.Linq;
using Xunit;
using Xunit.Extensions;
using Moq;
using FW.Core.Infrastructure;
using System.Collections.Generic;

namespace FW.Core.Tests
{
    public class MongoRepositoryTest
    {
        private IRepository<User> userRepository;

        public MongoRepositoryTest()
        {
            Settings settings = new Settings(new Dictionary<string, string>() { 
                { "ConnectionString", "mongodb://localhost:27017" },
                { "DatabaseName", "TestFW" }
            });

            userRepository = new MongoRepository<User>(settings);
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
