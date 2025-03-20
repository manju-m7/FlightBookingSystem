using FlightBookingSystem.Domain.Events;
using FlightBookingSystem.Infrastructure.MessageBroker;
using MediatR;

namespace FlightBookingSystem.Application.Handlers
{
	public class FlightCreatedEventHandler : INotificationHandler<FlightCreatedEvent>
	{
		private readonly RabbitMqPublisher _rabbitMqPublisher;

		public FlightCreatedEventHandler(RabbitMqPublisher rabbitMqPublisher )
        {
			_rabbitMqPublisher = rabbitMqPublisher;
		}
        public async Task Handle(FlightCreatedEvent notification, CancellationToken cancellationToken)
		{
			_rabbitMqPublisher.Publish( notification );
			 Console.WriteLine($"Flight ceated with ID {notification.FlightId} with origin {notification.Origin} and {notification.Destination}");
			 await Task.CompletedTask;
		}
	}
}
