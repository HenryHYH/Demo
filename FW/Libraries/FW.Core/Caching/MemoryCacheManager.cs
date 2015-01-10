using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FW.Core.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        private static readonly object _lock = new object();

        private ObjectCache Cache { get { return MemoryCache.Default; } }

        public void Set(string key, object value, int cacheTime = 30)
        {
            if (null == value)
                return;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Set(new CacheItem(key, value), policy);
        }

        public T Get<T>(string key, Func<T> defaultValue = null)
        {
            lock (_lock)
            {
                if (Contain(key))
                {
                    return (T)Cache.Get(key);
                }
                else if (null == defaultValue)
                {
                    return default(T);
                }

                var value = defaultValue();
                Set(key, value);

                return value;
            }
        }

        public bool Contain(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    Remove(item.Key);
        }

        public void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }
    }
}
