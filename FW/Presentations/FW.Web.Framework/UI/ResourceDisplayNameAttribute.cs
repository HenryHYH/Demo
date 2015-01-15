namespace FW.Web.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core.Infrastructure;
    using FW.Service.Localization;
    using FW.Web.Framework.MVC;

    public class ResourceDisplayNameAttribute : DisplayNameAttribute, IModelAttribute
    {
        #region Fields

        private string resourceKey = string.Empty;

        #endregion Fields

        #region Constructors

        public ResourceDisplayNameAttribute(string resourceKey)
            : base(resourceKey)
        {
            this.resourceKey = resourceKey;
        }

        #endregion Constructors

        #region Properties

        public override string DisplayName
        {
            get
            {
                var displayName = EngineContext.Current.Resolve<ILocalizationService>()
                                                        .GetResource(resourceKey);

                if (string.IsNullOrWhiteSpace(displayName))
                    return resourceKey;
                else
                    return displayName;
            }
        }

        public string Name
        {
            get { return "ResourceDisplayName"; }
        }

        #endregion Properties
    }
}