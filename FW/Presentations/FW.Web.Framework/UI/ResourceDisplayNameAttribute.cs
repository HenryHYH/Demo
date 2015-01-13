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
                return EngineContext.Current
                    .Resolve<ILocalizationService>()
                    .GetResource(resourceKey);
            }
        }

        public string Name
        {
            get { return "ResourceDisplayName"; }
        }
    }
}
