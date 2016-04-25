using System;
using System.Collections.Generic;

namespace MS.Infrastructure
{
    public class Singleton<T> : Singleton
    {
        static T instance;

        public static T Instance
        {
            get { return instance; }
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        public new static IList<T> Instance
        {
            get { return Singleton<IList<T>>.Instance; }
        }
    }

    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonDictionary()
        {
            Singleton<IDictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        public new static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<IDictionary<TKey, TValue>>.Instance; }
        }
    }

    public class Singleton
    {
        static readonly IDictionary<Type, object> allSingletons;

        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }

        public static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletons; }
        }
    }
}
