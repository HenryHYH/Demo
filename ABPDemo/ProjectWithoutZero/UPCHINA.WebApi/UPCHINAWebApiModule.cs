using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace UPCHINA
{
    [DependsOn(typeof(AbpWebApiModule), typeof(UPCHINAApplicationModule))]
    public class UPCHINAWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(UPCHINAApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
