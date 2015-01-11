namespace FW.Core.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Hosting;
    using System.Xml.Linq;

    public class Settings
    {
        #region Fields

        private const string fileName = "Setting.xml";

        private Dictionary<string, string> settings = new Dictionary<string, string>();

        #endregion Fields

        #region Constructors

        public Settings(Dictionary<string, string> settings)
        {
            this.settings = settings;
        }

        #endregion Constructors

        #region Methods

        public string GetSetting(string key)
        {
            return settings.ContainsKey(key) ?
                settings[key] :
                string.Empty;
        }

        #endregion Methods
    }
}