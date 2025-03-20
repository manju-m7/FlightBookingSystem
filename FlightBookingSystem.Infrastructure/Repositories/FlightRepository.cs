using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.Repositories;
using FlightBookingSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystem.Infrastructure.Repositories
{
	public class FlightRepository : IFlightRepository
	{
		private readonly FlightBookingDbContext _context;

		public FlightRepository(FlightBookingDbContext context) 
		{
			_context = context;
		}

		public async Task<Flight> AddFlightAsync(Flight flight, CancellationToken cancellationToken)
		{
			await _context.Flights.AddAsync(flight);
			//await _context.SaveChangesAsync(cancellationToken);
			return flight;
		}

		public async Task<List<Flight>> GetAllFlightsAsync()
		{
			return await _context.Flights.ToListAsync();
		}

		public async Task<Flight> GetFlightByIdAsync(Guid id)
		{
			return await _context.Flights.Include(f => f.Route).FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
