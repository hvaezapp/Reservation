using MassTransit;
using Microsoft.EntityFrameworkCore;
using Reservation.Infrastructure.Persistence.Context;
using System.Text.Json;

namespace Reservation.Services
{
    public class OutboxPublisherService(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ReservationDbContext>();
                var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var outboxMessages = await dbContext.Outboxs
                                                    .Where(a => !a.IsProcessed)
                                                    .OrderBy(o => o.Id)
                                                    .Take(10)
                                                    .ToListAsync(cancellationToken);

                foreach (var outboxMessage in outboxMessages)
                {
                    try
                    {

                        switch (outboxMessage.EventType)
                        {
                            case EventType.Notification:
                                await publishEndpoint.Publish(JsonSerializer.Deserialize<NotificationEvent>(outboxMessage.Message), cancellationToken);
                                break;

                            case EventType.Logging:
                                break;

                            case EventType.Integration:
                                break;

                            default:
                                Console.WriteLine($"Unsupported event type: {outboxMessage.EventType} (Id: {outboxMessage.Id})");
                                continue;
                        }

                        outboxMessage.RetryCount += 1;
                        outboxMessage.IsProcessed = true;
                        outboxMessage.ProcessOn = DateTime.UtcNow;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error publishing message {outboxMessage.Id} : {ex.Message}");
                        outboxMessage.ErrorMessage = ex.Message;
                    }

                    // update outbox table
                    dbContext.Update(outboxMessage);
                }

                await dbContext.SaveChangesAsync(cancellationToken);

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }


    }

}