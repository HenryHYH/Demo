namespace FW.Core.Infrastructure
{
    public interface IEngine
    {
        void Initialize();

        T Resolve<T>() where T : class;
    }
}
