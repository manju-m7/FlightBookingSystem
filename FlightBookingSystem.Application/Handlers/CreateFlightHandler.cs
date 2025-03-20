using AutoMapper;
using FlightBookingSystem.Application.Commands;
using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Domain.Aggregates;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.Repositories;
using FlightBookingSystem.Domain.SeedWork;
using MediatR;

namespace FlightBookingSystem.Application.Handlers
{
    public class CreateFlightHandler : IRequestHandler<CreateFlightCommand, FlightDto>
	{
		private readonly IFlightRepository _flightRepository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public CreateFlightHandler(IFlightRepository flightRepository, IMapper mapper, IUnitOfWork unitOfWork) 
		{
			_flightRepository = flightRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<FlightDto> Handle (CreateFlightCommand request, CancellationToken cancellationToken)
		{
			var flight = new Flight();
			flight = _mapper.Map<Flight>(request);
			flight = await _flightRepository.AddFlightAsync(flight, cancellationToken);
			await flight.InitializeFlightRoot(flight, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);
			return _mapper.Map<FlightDto>(flight);
		}
	}
}
