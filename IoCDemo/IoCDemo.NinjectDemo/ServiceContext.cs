using IoCDemo.Services;

namespace IoCDemo.NinjectDemo
{
    public class ServiceContext
    {
        public static IEngine Current
        {
            get
            {
                if (null == Singleton<IEngine>.Instance)
                    Init();

                return Singleton<IEngine>.Instance;
            }
        }

        private static void Init()
        {
            if (null == Singleton<IEngine>.Instance)
                Singleton<IEngine>.Instance = new NinjectEngine();
        }
    }
}
