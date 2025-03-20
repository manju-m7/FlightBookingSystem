using AutoMapper;
using FlightBookingSystem.Application.Commands;
using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.ValueObjects;

namespace FlightBookingSystem.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<Flight,FlightDto>()
				.ForMember(dest => dest.Origin, opt=>opt.MapFrom(src=> src.Route.Origin))
				.ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Route.Destination));

			CreateMap<CreateFlightCommand, Flight>()
				.ForMember(
							dest => dest.Route,
							opt => opt.MapFrom(src =>
								new Route
								{
									Origin = src.Origin,
									Destination = src.Destination
								}));
			
		}
	}
}
