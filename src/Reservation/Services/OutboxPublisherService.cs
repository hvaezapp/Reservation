
using Reservation.Infrastructure.Persistence.Context;

namespace Reservation.Services
{
    public class OutboxPublisherService(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
           return Task.CompletedTask;
        }
    }
}
