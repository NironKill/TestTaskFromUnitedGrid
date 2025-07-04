using Chat.Application.Common.Enums;
using Chat.Application.DTOs.Member;
using Chat.Application.Repositories.Interfaces;
using Chat.Infrastructure.RabbitMQ.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading;

namespace Chat.Infrastructure.RabbitMQ.Processor
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMemberRepository _member;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor(IMemberRepository member, ILogger<EventProcessor> logger)
        {
            _member = member;
            _logger = logger;
        }

        public async Task ProcessEvent(string message, CancellationToken cancellationToken)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.UserPublished:
                    await AddMember(message, cancellationToken);
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
                case "Platform_Published":
                    _logger.LogInformation("--> Platform Published Event Detected");
                    return EventType.UserPublished;
                default:
                    _logger.LogWarning("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }
        private async Task AddMember(string userPublishedMessage, CancellationToken cancellationToken)
        {
            MemberSubDTO sub = JsonSerializer.Deserialize<MemberSubDTO>(userPublishedMessage);

            try
            {
                MemberCreateDTO dto = new MemberCreateDTO()
                {
                    UserId = sub.Id,
                    UserName = sub.UserName
                };

                await _member.Create(dto, default);
                _logger.LogInformation("--> Member added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Could not add Member to DB {ex.Message}");
            }           
        }
    }
}
