using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FlightBookingSystem.Infrastructure.MessageBroker
{
	public class RabbitMqPublisher
	{
		private readonly RabbitMqSettings _rabbitMqSettings;
		private readonly IOptions<RabbitMqSettings> rabbitMqSettings;

		public RabbitMqPublisher(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
			_rabbitMqSettings = rabbitMqSettings.Value;
		}

		public void Publish<T> (T domainEvent)
		{
			var factory = new ConnectionFactory()
			{
				HostName = _rabbitMqSettings.HostName,
				UserName = _rabbitMqSettings.UserName,
				Password = _rabbitMqSettings.Password,
				Port = _rabbitMqSettings.Port,
				VirtualHost = _rabbitMqSettings.VirtualHost
			};

			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			channel.QueueDeclare(queue: _rabbitMqSettings.QueueName, durable:false, exclusive:false, autoDelete: false, arguments: null);
			var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(domainEvent));
			channel.BasicPublish(exchange:"",routingKey: _rabbitMqSettings.QueueName, basicProperties: null,body: body);

		}
    }
}
