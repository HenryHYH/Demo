using Abp.Web.Mvc.Views;

namespace Demo.Web.Views
{
    public abstract class DemoWebViewPageBase : DemoWebViewPageBase<dynamic>
    {

    }

    public abstract class DemoWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected DemoWebViewPageBase()
        {
            LocalizationSourceName = DemoConsts.LocalizationSourceName;
        }
    }
}