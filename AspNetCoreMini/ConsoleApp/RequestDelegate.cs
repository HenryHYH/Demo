using System.Threading.Tasks;

namespace ConsoleApp
{
    public delegate Task RequestDelegate(HttpContext context);
}