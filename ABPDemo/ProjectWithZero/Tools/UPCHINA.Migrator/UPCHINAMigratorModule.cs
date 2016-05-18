using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using UPCHINA.EntityFramework;

namespace UPCHINA.Migrator
{
    [DependsOn(typeof(UPCHINADataModule))]
    public class UPCHINAMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<UPCHINADbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}