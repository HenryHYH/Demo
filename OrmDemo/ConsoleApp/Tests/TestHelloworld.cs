using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Models;
using Xunit;

namespace ConsoleApp.Tests
{
    public class TestHelloworld : BaseTest
    {
        private readonly IQueryable<Book> query;

        public TestHelloworld()
        {
            query = this.db.Source<Book>();
        }

        [Fact]
        public void TestWhere()
        {
            var list = query.Where(x => x.Name == "Hello world")
                //.Where(x => x.Price > 0)
                //.Where(x => x.Enabled)
                            .OrderBy(x => x.Id)
                            .ToList();

            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void TestOrderBy()
        {
            var list = query.OrderByDescending(x => x.Id)
                            .ToList();

            Assert.Equal(1, list.FirstOrDefault().Id);
        }
    }
}
