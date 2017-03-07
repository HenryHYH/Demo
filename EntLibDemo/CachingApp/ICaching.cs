using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingApp
{
    public interface ICaching
    {
        T Get<T>(string key, Func<T> init = null);

        void Set(string key, object value);

        bool Contains(string key);
    }
}
