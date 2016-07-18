using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ConsoleApp.Infrastructure;
using ConsoleApp.Model;

namespace ConsoleApp
{
    public class TestOrm : BaseTest
    {
        [Fact]
        public void TestInsert()
        {
            var sql = DynamicQuery.GetInsertQuery("User", new User()
            {
                Name = "Hello world"
            });

            Assert.Equal("INSERT INTO [User] (Name) OUTPUT inserted.Id VALUES (@Name)", sql);
        }

        [Fact]
        public void TestUpdate()
        {
            var sql = DynamicQuery.GetUpdateQuery("User", new User()
            {
                Id = 1,
                Name = "Hello world"
            });

            Assert.Equal("UPDATE [User] SET Name = @Name WHERE Id = @Id", sql);
        }

        [Fact]
        public void TestQueryLT()
        {
            var query = DynamicQuery.GetDynamicQuery<User>("User", x => x.Id > 10);

            Assert.Equal("SELECT * FROM [User] WITH(NOLOCK) WHERE Id > @Id", query.Sql);
            Assert.Equal(10, query.Param.Id);
        }

        [Fact]
        public void TestQueryEq()
        {
            var query = DynamicQuery.GetDynamicQuery<User>("User", x => x.Name == "Hello");

            Assert.Equal("SELECT * FROM [User] WITH(NOLOCK) WHERE Name = @Name", query.Sql);
            Assert.Equal("Hello", query.Param.Name);
        }
    }
}
