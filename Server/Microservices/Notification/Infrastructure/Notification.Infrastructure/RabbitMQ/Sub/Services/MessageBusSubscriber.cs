﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Notification.Infrastructure.RabbitMQ.Sub.Processor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Notification.Infrastructure.RabbitMQ.Sub.Services
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private readonly ILogger<MessageBusSubscriber> _logger;

        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor, ILogger<MessageBusSubscriber> logger)
        {
            _configuration = configuration;
            _eventProcessor = eventProcessor;
            _logger = logger;

            InitializeRabbitMQ();
        }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }

            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                _logger.LogInformation("--> Event Received!");

                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage, stoppingToken);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName,
                exchange: "trigger",
                routingKey: "");

            _logger.LogInformation("--> Listenting on the Message Bus...");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShitdown;
        }
        private void RabbitMQ_ConnectionShitdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation("--> Connection Shutdown");
        }
    }
}
