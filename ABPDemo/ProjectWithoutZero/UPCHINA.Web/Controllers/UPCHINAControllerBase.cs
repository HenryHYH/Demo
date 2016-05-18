using Abp.Web.Mvc.Controllers;

namespace UPCHINA.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class UPCHINAControllerBase : AbpController
    {
        protected UPCHINAControllerBase()
        {
            LocalizationSourceName = UPCHINAConsts.LocalizationSourceName;
        }
    }
}