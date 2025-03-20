using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystem.Infrastructure.Persistence
{
	public class FlightBookingDbContext : DbContext , IUnitOfWork
	{
		private readonly IMediator _mediator;

		public FlightBookingDbContext(DbContextOptions<FlightBookingDbContext> options, IMediator mediator) : base(options) 
		{
			_mediator = mediator;
		}
		public DbSet<Flight> Flights { get; set; }
		//public DbSet<Availability> Availabilities { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Flight>(entity =>
			{
				entity.HasKey(f => f.Id);
				entity.OwnsOne(f => f.Route, route =>
				{
					route.Property(r => r.Origin).HasMaxLength(50);
					route.Property(route => route.Destination).HasMaxLength(50);
				});
			});
			//modelBuilder.Entity<Availability>().HasKey(a=>a.flightId);
			//modelBuilder.Entity<Availability>().HasOne(a => a.Flight).WithMany(f=>f.Availabilities).HasForeignKey(a=>a.flightId).IsRequired();
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			var domainEvents = ChangeTracker.Entries<AggregateRoot>().ToList()
				.SelectMany(e=>e.Entity.DomainEvents).ToList();
			foreach(var entry in ChangeTracker.Entries<AggregateRoot>())
			{
				entry.Entity.ClearDomainEvents();
			}
			var result = await base.SaveChangesAsync();
			foreach (var domainEvent in domainEvents)
			{
				await _mediator.Publish(domainEvent);
			}
			return result;
		}
	}
}
