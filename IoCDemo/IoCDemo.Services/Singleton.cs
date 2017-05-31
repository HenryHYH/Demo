using System;
using System.Collections.Generic;

namespace IoCDemo.Services
{
    public class Singleton
    {
        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }

        static readonly IDictionary<Type, object> allSingletons;

        protected static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletons; }
        }
    }

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
}
