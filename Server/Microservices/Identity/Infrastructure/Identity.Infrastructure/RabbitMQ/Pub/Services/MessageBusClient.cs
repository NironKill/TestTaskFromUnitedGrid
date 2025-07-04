using Identity.Application.DTOs.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Identity.Infrastructure.RabbitMQ.Pub.Services
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<MessageBusClient> _logger;

        public MessageBusClient(IConfiguration configuration, ILogger<MessageBusClient> logger)
        {
            _configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                logger.LogInformation("--> Connected to MessageBus");

            }
            catch (Exception ex)
            {
                logger.LogError($"--> Could not connect to the Message Bus: {ex.Message}");
            }

            _logger = logger;
        }

        public void PublishNewUser(UserPublishedDTO dto)
        {
            string message = JsonSerializer.Serialize(dto);

            if (_connection.IsOpen)
            {
                _logger.LogInformation("--> RabbitMQ Connection Open, sending message...");
                SendMessage(message);
            }
            else
                _logger.LogWarning("--> RabbitMQ connectionis closed, not sending");
        }

        private void SendMessage(string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
            _logger.LogInformation($"--> We have sent {message}");
        }
        public void Dispose()
        {
            _logger.LogInformation("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) => _logger.LogInformation("--> RabbitMQ Connection Shutdown");     
    }
}
