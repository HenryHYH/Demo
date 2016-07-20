using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;
using ConsoleApp.MyOrm;
using Xunit;

namespace ConsoleApp
{
    public class TestMyOrm : BaseTest
    {
        #region Fields

        private readonly QueryCommand<User> command;

        #endregion

        #region Ctor

        public TestMyOrm()
        {
            this.command = new QueryCommand<User>();
        }

        #endregion

        [Fact]
        public void TestNoCondition()
        {
            var list = command.ToList();

            Assert.Equal(4, list.Count);
        }

        [Fact]
        public void TestSelectWhereOrderBy()
        {
            var list = command.Where(x => x.Name.Contains("abc"))
                                .Where(x => x.Age > 1)
                                .OrderByDescding(x => x.Id)
                                .ToList();

            Assert.Equal(3, list.Count);
            Assert.Equal(4, list[0].Age);
        }
    }
}
