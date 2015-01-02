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
    public class SettingsManager
    {
        private const string fileName = "Settings.xml";

        #region Methods

        public Settings LoadSettings(string filePath = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = Path.Combine(MapPath("~/App_Data"), fileName);
            }

            Dictionary<string, string> settings = null;

            if (File.Exists(filePath))
            {
                var doc = XDocument.Load(filePath);
                settings = doc.Element("Settings")
                    .Elements()
                    .ToDictionary(key => key.Name.LocalName, val => val.Value);
            }
            else
            {
                settings = new Dictionary<string, string>();
            }

            return new Settings(settings);
        }

        #endregion

        #region Utilities

        private string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }
            else
            {
                //not hosted. For example, run in unit tests
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, path);
            }
        }

        #endregion
    }
}
