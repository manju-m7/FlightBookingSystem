using MediatR;

namespace FlightBookingSystem.Domain.Events
{
	public class FlightCreatedEvent : INotification
	{
        public Guid FlightId { get;}
        public string Origin { get;}
        public string Destination { get;}
        public DateTime DepartureTime { get; set; }
        public FlightCreatedEvent(Guid flightId, string origin, string destination, DateTime departureTime)
        {
            FlightId = flightId;
            Origin = origin;
            Destination = destination;
            DepartureTime = departureTime;
        }
    }
}
