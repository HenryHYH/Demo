using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;
using Dapper;
using Xunit;

namespace ConsoleApp
{
    public class TestQuery : BaseTest
    {
        [Fact]
        public void TestList()
        {
            var list = conn.Query<Customer>("SELECT Id, CTime, Name FROM Customer")
                            .ToList();

            Assert.True(list.Count > 0);
            Assert.True(list.Min(x => x.Id) == 1);
        }

        [Fact]
        public void TestCondition()
        {
            var list = conn.Query<Customer>("SELECT * FROM Customer WHERE Name LIKE @name", new { name = "hello%" })
                            .ToList();

            Assert.True(list.Count > 0);
        }
    }
}
