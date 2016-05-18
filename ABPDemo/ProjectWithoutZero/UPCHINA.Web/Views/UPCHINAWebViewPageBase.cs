using Abp.Web.Mvc.Views;

namespace UPCHINA.Web.Views
{
    public abstract class UPCHINAWebViewPageBase : UPCHINAWebViewPageBase<dynamic>
    {

    }

    public abstract class UPCHINAWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected UPCHINAWebViewPageBase()
        {
            LocalizationSourceName = UPCHINAConsts.LocalizationSourceName;
        }
    }
}