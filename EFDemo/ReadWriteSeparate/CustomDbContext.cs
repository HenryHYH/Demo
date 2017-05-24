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
            //DbInterception.Add(new DbMasterSlaveConnectionInterceptor());
            DbInterception.Add(new DbMasterSlaveCommandInterceptor());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().ToTable("Test").HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
