namespace FW.Core.Infrastructure
{
    public interface IStartupTask
    {
        /// <summary>
        /// Execute task
        /// </summary>
        void Execute();

        /// <summary>
        /// Order
        /// </summary>
        int Order { get; }
    }
}
