namespace IoCDemo.Services
{
    public interface IEngine
    {
        T Resolve<T>();
    }
}
