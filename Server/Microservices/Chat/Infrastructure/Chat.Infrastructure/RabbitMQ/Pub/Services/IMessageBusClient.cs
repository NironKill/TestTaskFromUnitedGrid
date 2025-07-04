using Chat.Application.DTOs.Message;

namespace Chat.Infrastructure.RabbitMQ.Pub.Services
{
    public interface IMessageBusClient
    {
        void PublishUnreadMessage(MessagePublishedDTO dto);
    }
}
