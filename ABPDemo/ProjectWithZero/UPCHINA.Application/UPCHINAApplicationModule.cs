using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace UPCHINA
{
    [DependsOn(typeof(UPCHINACoreModule), typeof(AbpAutoMapperModule))]
    public class UPCHINAApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
