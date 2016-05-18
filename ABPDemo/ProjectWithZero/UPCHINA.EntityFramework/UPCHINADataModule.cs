using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using UPCHINA.EntityFramework;

namespace UPCHINA
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(UPCHINACoreModule))]
    public class UPCHINADataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<UPCHINADbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
