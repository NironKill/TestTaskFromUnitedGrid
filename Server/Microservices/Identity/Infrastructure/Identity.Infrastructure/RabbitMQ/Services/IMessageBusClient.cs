using Identity.Application.DTOs.User;

namespace Identity.Infrastructure.RabbitMQ.Services
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(UserPublishedDTO dto);
    }
}
