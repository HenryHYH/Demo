using System.Data.Entity.Migrations;

namespace UPCHINA.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UPCHINA.EntityFramework.UPCHINADbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "UPCHINA";
        }

        protected override void Seed(UPCHINA.EntityFramework.UPCHINADbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
