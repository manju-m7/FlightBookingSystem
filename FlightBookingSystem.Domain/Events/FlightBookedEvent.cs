using MediatR;

namespace FlightBookingSystem.Domain.Events
{
	public class FlightBookedEvent : INotification
	{
        public Guid FlightId { get;}
        public string UserEmail { get; }
        public FlightBookedEvent(Guid flightId, string userEmail)
        {
            FlightId = flightId;
            UserEmail = userEmail;
        }
        
    }
}
