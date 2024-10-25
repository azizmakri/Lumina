using Lumina.Authentification.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Lumina.Authentification.Infrastructure.Persistence
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly RabbitMQSettings _rabbitMQSettings;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQConsumer(IOptions<RabbitMQSettings> rabbitMQSettings, IServiceScopeFactory serviceScopeFactory)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = _rabbitMQSettings.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMQSettings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var parts = message.Split(':');
                    var userId = parts[0];
                    var gradeFK = int.Parse(parts[1]);

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                        var user = await userManager.FindByIdAsync(userId);
                        if (user != null)
                        {
                            user.GradeFK = gradeFK;
                            await userManager.UpdateAsync(user);
                        }
                    }
                };

                channel.BasicConsume(queue: _rabbitMQSettings.QueueName, autoAck: true, consumer: consumer);

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }
    }
}
