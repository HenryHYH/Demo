﻿namespace FW.Core.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICacheManager
    {
        #region Methods

        void Clear();

        bool Contain(string key);

        T Get<T>(string key, Func<T> defaultValue = null);

        void Remove(string key);

        void RemoveByPattern(string pattern);

        void Set(string key, object value, int cacheTime = 60);

        #endregion Methods
    }
}