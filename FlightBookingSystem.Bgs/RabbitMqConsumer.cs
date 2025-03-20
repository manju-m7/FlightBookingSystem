using FlightBookingSystem.Domain.Events;
using FlightBookingSystem.Infrastructure.MessageBroker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FlightBookingSystem.Bgs
{
	public class RabbitMqConsumerService : BackgroundService
	{
		private readonly RabbitMqSettings _rabbitMqSettings;
		private readonly IOptions<RabbitMqSettings> rabbitMqSettings;
		private IConnection _connection;
		private IModel _channel;
		public RabbitMqConsumerService(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
			_rabbitMqSettings = rabbitMqSettings.Value;
			InitializeRabbitMQ();
		}
		public void InitializeRabbitMQ()
		{
			var factory = new ConnectionFactory()
			{
				HostName = _rabbitMqSettings.HostName,
				UserName = _rabbitMqSettings.UserName,
				Password = _rabbitMqSettings.Password,
				Port = _rabbitMqSettings.Port,
				VirtualHost = _rabbitMqSettings.VirtualHost
			};
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(queue: _rabbitMqSettings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += async (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				try
				{
					var flightEvent = JsonSerializer.Deserialize<FlightCreatedEvent>(message);
					if (flightEvent != null)
					{
						await GenerateTicket(flightEvent);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error processing message: {ex.Message}");
				}
			};
			_channel.BasicConsume(queue: _rabbitMqSettings.QueueName, autoAck: true, consumer: consumer);
			Console.WriteLine("Consumer started. Waiting for messages...");
			Console.ReadLine();
			return Task.CompletedTask;
		}

		private Task GenerateTicket(FlightCreatedEvent flightEvent)
		{
			Console.WriteLine($"Generating ticket for FlightID: {flightEvent.FlightId}, Destination: {flightEvent.Destination}");
			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			_channel?.Dispose();
			_connection?.Dispose();
			base.Dispose();
		}
	}
}
