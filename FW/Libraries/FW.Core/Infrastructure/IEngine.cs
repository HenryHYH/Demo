namespace FW.Core.Infrastructure
{
    public interface IEngine
    {
        #region Methods

        void Initialize();

        T Resolve<T>()
            where T : class;

        ContainerManager ContainerManager { get; }

        #endregion Methods
    }
}