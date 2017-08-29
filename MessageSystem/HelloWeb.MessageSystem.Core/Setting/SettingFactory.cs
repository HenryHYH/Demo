using System;
using System.Configuration;

namespace HelloWeb.MessageSystem.Core.Setting
{
    public static class SettingFactory
    {
        public static T Create<T>()
        {
            var setting = Activator.CreateInstance<T>();

            var type = typeof(T);
            var name = type.Name;
            var props = type.GetProperties();
            if (null != props)
            {
                foreach (var p in props)
                {
                    if (!p.CanWrite)
                        continue;

                    var key = $"{name}.{p.Name}";
                    var value = ConfigurationManager.AppSettings.Get(key);
                    if (!string.IsNullOrWhiteSpace(value))
                        p.SetValue(setting, value);
                }
            }

            return setting;
        }
    }
}
