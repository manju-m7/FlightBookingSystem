﻿using MediatR;

namespace FlightBookingSystem.Domain.SeedWork
{
	public abstract class AggregateRoot : Entity
	{
		private readonly List<INotification> _domainEvents = new();
		public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
		protected void AddDomainEvent(INotification domainEvent)
		{
			_domainEvents.Add(domainEvent);
		}
		public void ClearDomainEvents()
		{
			_domainEvents.Clear();
		}
    }
}
