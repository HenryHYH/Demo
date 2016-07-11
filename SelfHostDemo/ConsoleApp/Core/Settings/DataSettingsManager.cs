using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Core.Settings
{
    public partial class DataSettingsManager
    {
        #region Fields

        private const char SEPARATOR = ':';
        private const string FILE_NAME = "Settings.txt";

        #endregion

        #region Methods

        public virtual DataSettings LoadSettings(string filePath = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                filePath = Path.Combine(MapPath("~/App_Data/"), FILE_NAME);
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                return ParseSettings(text);
            }
            else
                return new DataSettings();
        }

        protected virtual string MapPath(string path)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');

            return Path.Combine(baseDirectory, path);
        }

        protected virtual DataSettings ParseSettings(string text)
        {
            var shellSettings = new DataSettings();
            if (string.IsNullOrWhiteSpace(text))
                return shellSettings;

            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while (null != (str = reader.ReadLine()))
                    settings.Add(str);
            }

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(SEPARATOR);
                if (-1 == separatorIndex)
                    continue;

                string key = setting.Substring(0, separatorIndex).Trim();
                string value = setting.Substring(separatorIndex + 1).Trim();

                shellSettings.RawSettings.Add(key, value);
            }

            return shellSettings;
        }

        #endregion
    }
}
