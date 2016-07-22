using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SQLinq;
using SQLinq.Dapper;
using ConsoleApp.Model;
using Dapper;

namespace ConsoleApp.SQLinqTests
{
    public class TestHelloworld : BaseTest
    {
        [Fact]
        public void Test()
        {
            var query = new SQLinq<User>()
                                .Where(x => x.Age > 1)
                                .Where(x => x.Enabled == true)
                                .OrderBy(x => x.Id);

            var list = conn.Query(query).ToList();

            Assert.Equal(2, list.Count);
        }
    }
}
