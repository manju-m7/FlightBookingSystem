using FlightBookingSystem.Application.DTOs;
using MediatR;

namespace FlightBookingSystem.Application.Queries
{
	public class GetFlightByIdQuery : IRequest<FlightDto>
	{
        public Guid FlightId { get; set; }
        public GetFlightByIdQuery(Guid flightId)
        {
            FlightId = flightId;
        }
    }
}
