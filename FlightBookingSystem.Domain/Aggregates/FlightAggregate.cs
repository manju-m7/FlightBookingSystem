using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.Events;
using FlightBookingSystem.Domain.SeedWork;

namespace FlightBookingSystem.Domain.Aggregates
{
	public class FlightAggregate : AggregateRoot
	{
		private readonly List<Flight> _flights = new();
		public IReadOnlyCollection<Flight> Flights => _flights.AsReadOnly();
		public void AddFlight(Flight flight)
		{
			if (_flights.Any(f => f.Id == flight.Id))
				throw new InvalidOperationException("Flight already exists");
            _flights.Add(flight);
			AddDomainEvent(new FlightCreatedEvent(Id, flight.Route.Origin, flight.Route.Destination, flight.DepartureTime));
		}
		public void RemoveFlight(Guid flightId)
		{
			var flight = _flights.FirstOrDefault(x => x.Id == flightId);
			if (flight == null)
				throw new InvalidOperationException("Flight not found");
			_flights.Remove(flight);
		}

	}
}
