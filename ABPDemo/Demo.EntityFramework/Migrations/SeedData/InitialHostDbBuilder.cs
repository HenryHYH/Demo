using Demo.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Demo.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly DemoDbContext _context;

        public InitialHostDbBuilder(DemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
