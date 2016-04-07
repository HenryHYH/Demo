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
    public class TestExecute : BaseTest
    {
        [Fact]
        public void TestInsert()
        {
            var customer = new Customer() { Name = "Hello world" };
            var result = conn.Execute("INSERT INTO Customer (Name) VALUES (@name)", customer);

            Assert.Equal(1, result);
        }

        [Fact]
        public void TestInsertTransaction()
        {
            int result = 0;

            using (var trans = conn.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Customer (Name) VALUES (@name);";
                    sql += "SELECT CAST(SCOPE_IDENTITY() AS int);";
                    var id = conn.Query<int>(sql, new { Name = "Parent" }, trans).SingleOrDefault();

                    sql = "INSERT INTO CustomerAttribute ([Key], [Value], CustomerId) VALUES (@key, @value, @customerId);";
                    result = conn.Execute(sql, new { Key = "Key", Value = "Value", CustomerId = id }, trans);

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
            }

            Assert.Equal(1, result);
        }

        [Fact]
        public void TestUpdate()
        {
            var customer = conn.Query<Customer>("SELECT * FROM Customer WHERE Id = @id", new { Id = 1 }).SingleOrDefault();
            if (null != customer)
            {
                customer.Name = "UPDATED";
                var result = conn.Execute("UPDATE Customer SET Name = @name, CTime = GETDATE() WHERE Id = @id", customer);

                Assert.Equal(1, result);
            }
        }

        [Fact]
        public void TestDelete()
        {
            var id = conn.Query<int>("SELECT TOP 1 Id FROM Customer ORDER BY Id DESC").SingleOrDefault();
            if (id > 1)
            {
                var result = conn.Execute("DELETE FROM Customer WHERE Id = @id", new { id = id });

                Assert.Equal(1, result);
            }
        }
    }
}
