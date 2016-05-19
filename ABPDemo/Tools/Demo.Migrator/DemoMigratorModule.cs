using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Demo.EntityFramework;

namespace Demo.Migrator
{
    [DependsOn(typeof(DemoDataModule))]
    public class DemoMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<DemoDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}