namespace FW.Web.Framework.ViewEngines.Razor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core.Infrastructure;
    using FW.Service.Localization;

    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        #region Fields

        private ILocalizationService localizationService;

        #endregion Fields

        #region Methods

        public override void InitHelpers()
        {
            base.InitHelpers();

            localizationService = EngineContext.Current.Resolve<ILocalizationService>();
        }

        public string T(string key, params string[] args)
        {
            string resource = localizationService.GetResource(key);

            if (!string.IsNullOrWhiteSpace(resource))
                if (null != args && args.Any())
                    return string.Format(resource, args);
                else
                    return resource;

            return key;
        }

        #endregion Methods
    }
}