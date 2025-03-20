using FlightBookingSystem.Domain.Events;
using FlightBookingSystem.Domain.SeedWork;
using FlightBookingSystem.Domain.ValueObjects;

namespace FlightBookingSystem.Domain.Entities
{
	public class Flight : AggregateRoot
	{
        public Guid Id { get; private set; }
        public string FlightNumber { get; private set; }
        public Route Route { get; private set; }
        public DateTime DepartureTime { get; private set; }
		public DateTime ArrivalTime { get; private set; }
		public decimal Price { get; private set; }
       // public ICollection<Availability> Availabilities { get; private set; }
        public Flight()
        {
            
        }

		public class FlightBuilder
		{
			private readonly Flight _flight;

			public FlightBuilder(Flight flight)
			{
				_flight = new Flight();
			}

			public FlightBuilder SetFlightNumber(string flightNumber)
            {
                _flight.FlightNumber = flightNumber;
                return this;
            }
            public FlightBuilder SetRoute(Route route)
            {
                _flight.Route = route;
                return this;
            }
            public FlightBuilder SetDepartureTime(DateTime departureTime)
            {
                _flight.DepartureTime = departureTime;
                return this;
            }
            public FlightBuilder SetArrivalTime(DateTime arrivalTime)
            {
                _flight.ArrivalTime = arrivalTime;
                return this;
            }

            public FlightBuilder SetPrice(decimal price)
            {
                _flight.Price = price;
                return this;
            }
            public Flight Build()
            {
                _flight.Id = _flight.Id == Guid.Empty? Guid.NewGuid() : _flight.Id;
                return _flight;
            }
        }

        public async Task<Flight> InitializeFlightRoot(Flight flightParams, CancellationToken cancellationToken)
        {
			Flight flight = new Flight.FlightBuilder(this)
			.SetFlightNumber(flightParams.FlightNumber)
            .SetRoute(flightParams.Route)
            .SetDepartureTime (flightParams.DepartureTime)
            .SetArrivalTime (flightParams.ArrivalTime)
            .SetPrice(flightParams.Price)
            .Build();
            AddDomainEvent(new FlightCreatedEvent(flight.Id, flight.Route.Origin, flight.Route.Destination, flight.DepartureTime));
            await Task.CompletedTask;
            return flight;
		}
        
        
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }
			Price = newPrice;
		}


    }
}
