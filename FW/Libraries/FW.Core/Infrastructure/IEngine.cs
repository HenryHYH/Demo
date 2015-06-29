namespace FW.Core.Infrastructure
{
    public interface IEngine
    {
        #region Properties

        ContainerManager ContainerManager
        {
            get;
        }

        #endregion Properties

        #region Methods

        void Initialize();

        T Resolve<T>()
            where T : class;

        T Resolve<T>(string name)
            where T : class;

        #endregion Methods
    }
}