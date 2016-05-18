using System.Reflection;
using Abp.Modules;

namespace UPCHINA
{
    [DependsOn(typeof(UPCHINACoreModule))]
    public class UPCHINAApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
