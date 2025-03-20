using FlightBookingSystem.Application.DTOs;
using MediatR;

namespace FlightBookingSystem.Application.Queries
{
    public class GetAllFlightsQuery : IRequest<List<FlightDto>>
    {

    }
}
