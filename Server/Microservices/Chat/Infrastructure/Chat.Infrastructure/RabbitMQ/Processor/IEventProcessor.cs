namespace Chat.Infrastructure.RabbitMQ.Processor
{
    public interface IEventProcessor
    {
        Task ProcessEvent(string message, CancellationToken cancellationToken);
    }
}
