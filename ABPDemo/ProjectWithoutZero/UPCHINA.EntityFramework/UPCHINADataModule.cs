using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using UPCHINA.EntityFramework;

namespace UPCHINA
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(UPCHINACoreModule))]
    public class UPCHINADataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<UPCHINADbContext>(null);
        }
    }
}
