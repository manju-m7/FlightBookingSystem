using FlightBookingSystem.Domain.Repositories;

namespace FlightBookingSystem.Domain.SeedWork
{
	public interface IUnitOfWork : IDisposable
	{
		//IFlightRepository FlightRepository { get; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
