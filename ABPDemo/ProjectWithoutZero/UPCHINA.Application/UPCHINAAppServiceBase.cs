using Abp.Application.Services;

namespace UPCHINA
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class UPCHINAAppServiceBase : ApplicationService
    {
        protected UPCHINAAppServiceBase()
        {
            LocalizationSourceName = UPCHINAConsts.LocalizationSourceName;
        }
    }
}