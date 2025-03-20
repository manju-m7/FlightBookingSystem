using FlightBookingSystem.Application.Commands;
using FluentValidation;

namespace FlightBookingSystem.Api
{
	public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
	{
        public CreateFlightCommandValidator()
        {
            RuleFor(x => x.FlightNumber).Length(3, 10).WithMessage("Flight number must be between 3 and 10 characters.");
            RuleFor(x => x.DepartureTime).GreaterThan(DateTime.Now).WithMessage("Departure Time must be in the future");
            RuleFor(x => x.ArrivalTime).GreaterThan(x => x.DepartureTime).WithMessage("Arrival Time must be greater than the Departure Time.");
        }
    }
}
