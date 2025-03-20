using FlightBookingSystem.Domain.Entities;

namespace FlightBookingSystem.Domain.Repositories
{
	public interface IFlightRepository
	{
		Task<List<Flight>> GetAllFlightsAsync();
		Task<Flight> AddFlightAsync(Flight flight, CancellationToken cancellationToken);
		Task<Flight> GetFlightByIdAsync(Guid id);
	}
}
