using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Core.Caching
{
    public interface ICacheManager
    {
        void Set(string key, object value, int cacheTime = 60);

        T Get<T>(string key, Func<T> defaultValue = null);

        bool Contain(string key);

        void Remove(string key);

        void RemoveByPattern(string pattern);

        void Clear();
    }
}
