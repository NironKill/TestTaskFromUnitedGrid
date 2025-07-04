namespace Notification.Infrastructure.RabbitMQ.Sub.Processor
{
    public interface IEventProcessor
    {
        Task ProcessEvent(string message, CancellationToken cancellationToken);
    }
}
