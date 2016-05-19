using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace Demo
{
    [DependsOn(typeof(DemoCoreModule), typeof(AbpAutoMapperModule))]
    public class DemoApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
