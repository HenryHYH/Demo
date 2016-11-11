using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Domains;
using Dapper.Extensions.Linq.Core.Repositories;
using Xunit;

namespace ConsoleApp.Tests
{
    public class Get : BaseTest
    {
        private readonly IRepository<User> repository;

        public Get()
        {
            this.repository = CONTAINER.Resolve<IRepository<User>>();
        }

        [Fact]
        public void Helloworld()
        {
            var user = repository.Get(5);
            Assert.Equal(5, user.Id);
            Assert.Equal(100, user.Age);
            Assert.Equal("Test", user.Name);
            Assert.True(user.Enabled);
        }

        [Fact]
        public void Query()
        {
            Expression<Func<User, bool>> predicate = x => x.Enabled && x.Age > 1;

            var query = repository.Query(predicate);
            var list = query.ToList();

            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void Max()
        {
            var max = int.Parse(repository.QueryScalar("SELECT MAX(Age) FROM [User] WITH(NOLOCK)").ToString());

            Assert.Equal(100, max);
        }
    }
}
