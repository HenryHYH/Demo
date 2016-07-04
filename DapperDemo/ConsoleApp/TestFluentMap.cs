using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Mapping;
using Dapper.FluentMap;
using Xunit;
using Dapper;
using ConsoleApp.Model;

namespace ConsoleApp
{
    public class TestFluentMap : BaseTest
    {
        public TestFluentMap()
        {
            FluentMapper.Initialize(x => x.AddMap(new UserMap()));
        }

        [Fact]
        public void Test()
        {
            var users = conn.Query<User>("SELECT UId, UName FROM [User] WITH(NOLOCK) ORDER BY UId");

            Assert.Equal(2, users.Count());
            Assert.Equal(1, users.FirstOrDefault().Id);
        }
    }
}
