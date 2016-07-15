using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Core.Settings;
using ConsoleApp.Core.Datas;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace ConsoleApp.Core.Caching
{
    public class RedisCacheManager : ICacheManager
    {
        #region Fields

        private readonly DataSettings dataSettings;
        private readonly ICacheClient client;

        #endregion

        #region Ctor

        public RedisCacheManager(DataSettings dataSettings)
        {
            this.dataSettings = dataSettings;
            this.client = RedisProvider.GetClient(dataSettings);
        }

        #endregion

        #region Methods

        public void Clear()
        {
            client.FlushDb();
        }

        public bool Contain(string key)
        {
            return client.Exists(key);
        }

        public T Get<T>(string key, Func<T> defaultValue = null, Func<T, bool> isEmpty = null)
        {
            if (Contain(key))
                return client.Get<T>(key);
            else if (null == defaultValue)
                return default(T);

            var value = defaultValue();
            if (null == isEmpty || !isEmpty(value))
                Set(key, value);

            return value;
        }

        public void Remove(string key)
        {
            client.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var keys = client.SearchKeys(pattern);
            if (null == keys || !keys.Any())
                return;

            foreach (var key in keys)
                Remove(key);
        }

        public void Set(string key, object value, int cacheTime = 60)
        {
            if (null == value)
                return;

            client.Add(key, value, new TimeSpan(0, cacheTime, 0));
        }

        #endregion
    }
}
