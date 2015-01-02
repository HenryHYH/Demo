using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Linq;

namespace FW.Core.Infrastructure
{
    public class Settings
    {
        #region Fields

        private const string fileName = "Setting.xml";
        private Dictionary<string, string> settings = new Dictionary<string, string>();

        #endregion

        #region Ctor

        public Settings(Dictionary<string, string> settings)
        {
            this.settings = settings;
        }

        #endregion

        #region Methods

        public string GetSetting(string key)
        {
            return settings.ContainsKey(key) ?
                settings[key] :
                string.Empty;
        }

        #endregion
    }
}
