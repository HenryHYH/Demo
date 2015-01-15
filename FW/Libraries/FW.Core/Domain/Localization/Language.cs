namespace FW.Core.Domain.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Language : BaseEntity
    {
        #region Properties

        public string Code
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}