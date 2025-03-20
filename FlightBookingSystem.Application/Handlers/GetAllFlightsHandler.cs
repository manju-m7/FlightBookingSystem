using AutoMapper;
using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Queries;
using FlightBookingSystem.Domain.Repositories;
using MediatR;

namespace FlightBookingSystem.Application.Handlers
{
    public class GetAllFlightsHandler : IRequestHandler<GetAllFlightsQuery,List<FlightDto>>
	{
		private readonly IFlightRepository _flightRepository;
		private readonly IMapper _mapper;

		public GetAllFlightsHandler(IFlightRepository flightRepository, IMapper mapper)
        {
			_flightRepository = flightRepository;
			_mapper = mapper;
		}
		public async Task<List<FlightDto>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
		{
			var flights = await _flightRepository.GetAllFlightsAsync();
			return _mapper.Map<List<FlightDto>>(flights);
		}
    }
}
