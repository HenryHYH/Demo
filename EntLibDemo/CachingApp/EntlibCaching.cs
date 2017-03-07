using Microsoft.Practices.EnterpriseLibrary.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingApp
{
    public class EntlibCaching : ICaching
    {
        private readonly ICacheManager cache = null;

        public EntlibCaching()
        {
            this.cache = CacheFactory.GetCacheManager();
        }

        public T Get<T>(string key, Func<T> init = null)
        {
            if (Contains(key))
                return (T)cache.GetData(key);
            if (null == init)
                return default(T);

            var value = init();
            Set(key, value);

            return value;
        }

        public void Set(string key, object value)
        {
            cache.Add(key, value);
        }

        public bool Contains(string key)
        {
            return cache.Contains(key);
        }
    }
}
