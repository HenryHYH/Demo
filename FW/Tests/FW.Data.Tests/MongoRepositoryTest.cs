namespace FW.Core.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using FW.Core.Data;
    using FW.Core.Domain.Users;
    using FW.Core.Infrastructure;
    using FW.Data;

    using Moq;

    using Xunit;
    using Xunit.Extensions;

    public class MongoRepositoryTest
    {
        #region Fields

        private IRepository<User> userRepository;

        #endregion Fields

        #region Constructors

        public MongoRepositoryTest()
        {
            Settings settings = new Settings(new Dictionary<string, string>() {
                { "ConnectionString", "mongodb://localhost:27017" },
                { "DatabaseName", "TestFW" }
            });

            userRepository = new MongoRepository<User>(settings);
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}