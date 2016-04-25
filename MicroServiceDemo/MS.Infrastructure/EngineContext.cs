using System.Runtime.CompilerServices;

namespace MS.Infrastructure
{
    public class EngineContext
    {
        #region Methods

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if (null == Singleton<IEngine>.Instance || forceRecreate)
            {
                Singleton<IEngine>.Instance = new Engine();
                var config = ServerConfig.GetConfiguration();
                Singleton<IEngine>.Instance.Initialize(config);
            }

            return Singleton<IEngine>.Instance;
        }

        #endregion

        #region Properties

        public static IEngine Current
        {
            get
            {
                if (null == Singleton<IEngine>.Instance)
                    Initialize(false);

                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
