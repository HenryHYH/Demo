using System.Reflection;
using Abp.Modules;

namespace UPCHINA
{
    public class UPCHINACoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
