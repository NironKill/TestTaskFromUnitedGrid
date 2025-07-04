using Identity.Application.DTOs.User;

namespace Identity.Infrastructure.RabbitMQ.Pub.Services
{
    public interface IMessageBusClient
    {
        void PublishNewUser(UserPublishedDTO dto);
    }
}
