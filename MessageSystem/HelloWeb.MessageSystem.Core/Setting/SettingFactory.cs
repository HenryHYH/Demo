using System;
using System.Collections.Specialized;
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
            var values = ConfigurationManager.GetSection(name) as NameValueCollection;
            if (null == values || 0 == values.Count)
                return setting;

            var props = type.GetProperties();
            if (null != props)
            {
                foreach (var p in props)
                {
                    if (!p.CanWrite)
                        continue;

                    var value = values.Get(p.Name);
                    if (string.IsNullOrWhiteSpace(value))
                        continue;

                    p.SetValue(setting, value);
                }
            }

            return setting;
        }
    }
}
