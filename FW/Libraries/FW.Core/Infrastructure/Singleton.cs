using System;

namespace FW.Core.Infrastructure
{
    public class Singleton<T> where T : class
    {
        private static T instance;

        private static readonly object locker = new object();

        public static T Instance
        {
            get
            {
                if (null == instance)
                    lock (locker)
                        if (null == instance)
                            instance = (T)Activator.CreateInstance(typeof(T), true);

                return instance;
            }
        }
    }
}
