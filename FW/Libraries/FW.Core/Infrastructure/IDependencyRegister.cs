namespace FW.Core.Infrastructure
{
    using Autofac;

    public interface IDependencyRegister
    {
        #region Properties

        int Order
        {
            get;
        }

        #endregion Properties

        #region Methods

        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        #endregion Methods
    }
}