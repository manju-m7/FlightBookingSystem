using AutoMapper;
using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Queries;
using FlightBookingSystem.Domain.Repositories;
using MediatR;

namespace FlightBookingSystem.Application.Handlers
{
	public class GetFlightByIdHandler : IRequestHandler<GetFlightByIdQuery, FlightDto>
	{
		private readonly IMapper _mapper;
		private readonly IFlightRepository _flightRepository;

		public GetFlightByIdHandler(IMapper mapper, IFlightRepository flightRepository)
        {
			_mapper = mapper;
			_flightRepository = flightRepository;
		}
		public async Task<FlightDto> Handle(GetFlightByIdQuery request, CancellationToken cancellation)
		{
			var flight = await _flightRepository.GetFlightByIdAsync(request.FlightId);
			return _mapper.Map<FlightDto>(flight);
		}
    }
}
