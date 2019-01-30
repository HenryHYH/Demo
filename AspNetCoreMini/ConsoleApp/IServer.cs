using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }
}
