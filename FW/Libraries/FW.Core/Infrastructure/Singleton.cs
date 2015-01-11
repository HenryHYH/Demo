namespace FW.Core.Infrastructure
{
    using System;

    public class Singleton<T>
        where T : class
    {
        #region Fields

        private static readonly object locker = new object();

        private static T instance;

        #endregion Fields

        #region Properties

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

        #endregion Properties
    }
}