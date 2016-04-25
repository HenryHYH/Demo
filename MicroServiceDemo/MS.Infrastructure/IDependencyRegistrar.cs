using Autofac;

namespace MS.Infrastructure
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder bulider, ITypeFinder typeFinder);

        int Order { get; }
    }
}
