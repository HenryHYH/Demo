using Autofac;

namespace FW.Core.Infrastructure
{
    public interface IDependencyRegister
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
