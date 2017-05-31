using IoCDemo.Services;
using Ninject;

namespace IoCDemo.NinjectDemo
{
    public class NinjectEngine : IEngine
    {
        private readonly IKernel kernel;

        public NinjectEngine()
        {
            kernel = new StandardKernel();
            kernel.Load("*.xml");
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}
