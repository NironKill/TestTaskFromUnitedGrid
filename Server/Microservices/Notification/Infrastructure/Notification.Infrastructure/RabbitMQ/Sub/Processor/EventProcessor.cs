using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notification.Infrastructure.Common.Enums;
using Notification.Infrastructure.Common.Models;
using Notification.Infrastructure.Email.Services;
using Notification.Infrastructure.RabbitMQ.Models;
using System.Text.Json;

namespace Notification.Infrastructure.RabbitMQ.Sub.Processor
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor(IServiceScopeFactory scopeFactory, ILogger<EventProcessor> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task ProcessEvent(string message, CancellationToken cancellationToken)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.NotificationPublished:
                    await AddNotification(message, cancellationToken);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            _logger.LogInformation("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<EventModel>(notifcationMessage);

            switch (eventType.Event)
            {
                case "Notification_Published":
                    _logger.LogInformation("--> Notification Published Event Detected");
                    return EventType.NotificationPublished;
                default:
                    _logger.LogWarning("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }
        private async Task AddNotification(string userPublishedMessage, CancellationToken cancellationToken)
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                IEmailService emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                NotificationSubDTO sub = JsonSerializer.Deserialize<NotificationSubDTO>(userPublishedMessage);

                try
                {
                    NotificationSendDTO dto = new NotificationSendDTO()
                    {
                        UnreadMessages = sub.UnreadMessages
                    };

                    await emailService.SendEmail(dto, cancellationToken);
                    _logger.LogInformation("--> Notification sender!");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"--> Could not Notification sender {ex.Message}");
                }
            }
        }
    }
}
