namespace FW.Core.Infrastructure
{
    public interface IStartupTask
    {
        #region Properties

        /// <summary>
        /// Order
        /// </summary>
        int Order
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Execute task
        /// </summary>
        void Execute();

        #endregion Methods
    }
}