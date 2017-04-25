using System.Data.Entity;

namespace DbTest
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(string nameOrConnectionString) :
            base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ModelConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
