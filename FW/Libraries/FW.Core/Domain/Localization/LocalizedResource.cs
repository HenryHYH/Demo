namespace FW.Core.Domain.Localization
{
    using FW.Core.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LocalizedResource : BaseEntity
    {
        #region Properties

        public int LanguageId
        {
            get; set;
        }

        public string ResourceKey
        {
            get; set;
        }

        public string ResourceValue
        {
            get; set;
        }

        #endregion Properties
    }
}