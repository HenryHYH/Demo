using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Mapping;
using ConsoleApp.Model;
using Dapper;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dapper.FluentMap.Dommel.Mapping;
using Xunit;

namespace ConsoleApp
{
    public class TestDommelFluentMap : BaseTest
    {
        public TestDommelFluentMap()
        {
            FluentMapper.Initialize(x =>
            {
                x.AddMap(new UserModelMap());
                x.ForDommel();
            });
        }

        [Fact]
        public void Test()
        {
            DommelEntityMap<UserModel> map = FluentMapper.EntityMaps[typeof(UserModel)] as DommelEntityMap<UserModel>;

            var users = conn.Query<UserModel>("SELECT UId, UName FROM [User] WITH(NOLOCK) ORDER BY UId");

            Assert.Equal(2, users.Count());
            Assert.Equal(1, users.FirstOrDefault().Id);
        }
    }
}
