using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;
using ConsoleApp.MyOrm;
using Xunit;
using Xunit.Abstractions;

namespace ConsoleApp
{
    public class TestMyOrm
    {
        #region Fields

        private readonly QueryCommand<User> command;
        private readonly ITestOutputHelper output;

        #endregion

        #region Ctor

        public TestMyOrm(ITestOutputHelper output)
        {
            this.output = output;
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

        [Fact]
        public void TestTake()
        {
            var list = command.Take(2)
                              .ToList();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void TestSkip()
        {
            var list = command.Skip(1)
                              .ToList();

            Assert.Equal(3, list.Count);
            Assert.Equal(2, list[0].Age);
        }

        [Fact]
        public void TestTakeSkip()
        {
            var list = command.OrderByDescding(x => x.Age)
                              .Skip(1)
                              .Take(2)
                              .ToList();
            output.WriteLine(command.GetSelectCommand().Item1);

            Assert.Equal(2, list.Count);
            Assert.Equal(3, list[0].Age);
        }

        [Fact]
        public void TestAggr()
        {
            Assert.Equal(4, command.Count(x => x.Name));
            Assert.Equal(1, command.CountDistinct(x => x.Name));
            Assert.Equal(10, command.Sum<int>(x => x.Age));
            Assert.Equal(4, command.Max<int>(x => x.Age));
            Assert.Equal(1, command.Min<int>(x => x.Age));
            Assert.Equal(2.5, command.Avg<double>(x => x.Age));
        }

        [Fact]
        public void TestAny()
        {
            Assert.True(command.Any());

            var any = command.Where(x => x.Id < 0).Any();
            Assert.False(any);
        }
    }
}
