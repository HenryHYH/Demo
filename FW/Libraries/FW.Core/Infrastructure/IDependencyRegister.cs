using Autofac;

namespace FW.Domain.Infrastructure
{
    public interface IDependencyRegister
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
