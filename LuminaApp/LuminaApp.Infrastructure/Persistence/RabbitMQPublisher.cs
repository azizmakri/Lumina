using LuminaApp.Domain.Entities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Infrastructure.Persistence
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQSettings _rabbitMQSettings;

        public RabbitMQPublisher(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        public void PublishGradeFK(string userId, int gradeFK)
        {
            var factory = new ConnectionFactory() { HostName = _rabbitMQSettings.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMQSettings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var message = $"{userId}:{gradeFK}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: _rabbitMQSettings.QueueName, basicProperties: null, body: body);
            }
        }
    }
}
