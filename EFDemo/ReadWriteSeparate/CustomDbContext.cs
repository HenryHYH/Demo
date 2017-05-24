using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

namespace ReadWriteSeparate
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer<CustomDbContext>(null);
            DbInterception.Add(new DbMasterSlaveCommandInterceptor());
            DbInterception.Add(new DbMasterSlaveConnectionInterceptor());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().ToTable("Test").HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
