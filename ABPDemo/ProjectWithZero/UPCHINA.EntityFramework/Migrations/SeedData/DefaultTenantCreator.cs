using System.Linq;
using UPCHINA.EntityFramework;
using UPCHINA.MultiTenancy;

namespace UPCHINA.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly UPCHINADbContext _context;

        public DefaultTenantCreator(UPCHINADbContext context)
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