using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.Domain.Settings
{
    public class DataSettings
    {
        #region Ctor

        public DataSettings()
        {
            this.RawSettings = new Dictionary<string, string>();
        }

        #endregion

        #region Properties

        public IDictionary<string, string> RawSettings { get; set; }

        #endregion
    }
}
