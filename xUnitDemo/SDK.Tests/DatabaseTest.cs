using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SDK.Tests
{
    public class DatabaseTest : IDisposable
    {
        private MyContext context;

        public DatabaseTest()
        {
#pragma warning disable
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            context = new MyContext(GetTestDbName());
            context.Database.Delete();
            context.Database.Create();
        }

        public void Dispose()
        {
            if (null != context)
                context.Dispose();
        }

        private string GetTestDbName()
        {
            return "Data Source=" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)) + @"\\SDK.Tests.Db.sdf;Persist Security Info=False";
        }

        private T SaveAndLoadEntity<T>(T entity, bool disposeContext = true) where T : class
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();

            if (disposeContext)
            {
                context.Dispose();
                context = new MyContext(GetTestDbName());
            }

            var fromDb = context.Set<T>().FirstOrDefault();
            return fromDb;
        }

        [Fact]
        public void TestDb()
        {
            var person = new Person() { Name = "Henry" };

            var fromDb = SaveAndLoadEntity(person);

            Assert.Equal(1, fromDb.Id);
            Assert.Equal("Henry", fromDb.Name);
        }
    }

    public class Person
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class MyContext : DbContext
    {
        public MyContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasKey<long>(x => x.Id);
        }
    }
}
