using FW.Core.Infrastructure;
using FW.Service.Localization;
using FW.Web.Framework.MVC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Web.Framework.UI
{
    public class ResourceDisplayNameAttribute : DisplayNameAttribute, IModelAttribute
    {
        private string resourceKey = string.Empty;

        public ResourceDisplayNameAttribute(string resourceKey)
            : base(resourceKey)
        {
            this.resourceKey = resourceKey;
        }

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
    }
}
