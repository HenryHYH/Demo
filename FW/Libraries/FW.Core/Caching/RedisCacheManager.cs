using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.Core.Data;
using ServiceStack.Redis;

namespace FW.Core.Caching
{
    public class RedisCacheManager : ICacheManager
    {
        #region Fields

        private static readonly object _lock = new object();

        private readonly DataSettings dataSettings;
        private readonly IRedisClient client;

        #endregion

        #region Ctor

        public RedisCacheManager(DataSettings dataSettings)
        {
            this.dataSettings = dataSettings;

            client = new RedisClient(dataSettings.RawDataSettings["RedisHost"]);
        }

        #endregion

        #region Methods

        public void Clear()
        {
            client.RemoveAll(client.GetAllKeys());
        }

        public bool Contain(string key)
        {
            return client.ContainsKey(key);
        }

        public T Get<T>(string key, Func<T> defaultValue = null)
        {
            lock (_lock)
            {
                if (Contain(key))
                    return client.Get<T>(key);
                else if (null == defaultValue)
                    return default(T);

                var value = defaultValue();
                Set(key, value);

                return value;
            }
        }

        public void Remove(string key)
        {
            client.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, int cacheTime = 60)
        {
            if (null == value)
                return;

            client.Set(key, value, new TimeSpan(0, cacheTime, 0));
        }

        #endregion
    }
}
