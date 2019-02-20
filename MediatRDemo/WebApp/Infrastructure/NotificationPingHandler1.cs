using MediatR;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public class NotificationPingHandler1 : INotificationHandler<NotificationPing>
    {
        public Task Handle(NotificationPing notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Pong 1");

            return Task.CompletedTask;
        }
    }
}
