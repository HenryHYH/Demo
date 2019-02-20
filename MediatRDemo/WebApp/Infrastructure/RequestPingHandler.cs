using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public class RequestPingHandler : IRequestHandler<RequestPing, string>
    {
        public Task<string> Handle(RequestPing request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }
}
