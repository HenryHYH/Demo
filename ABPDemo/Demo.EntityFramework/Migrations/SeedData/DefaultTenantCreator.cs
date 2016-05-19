using System.Linq;
using Demo.EntityFramework;
using Demo.MultiTenancy;

namespace Demo.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly DemoDbContext _context;

        public DefaultTenantCreator(DemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == "Default");
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = "Default", Name = "Default"});
                _context.SaveChanges();
            }
        }
    }
}