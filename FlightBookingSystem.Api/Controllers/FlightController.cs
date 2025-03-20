using FlightBookingSystem.Application.Commands;
using FlightBookingSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FlightController : ControllerBase
	{
		private readonly IMediator _mediator;

		public FlightController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllFlights()
		{
			var flights = await _mediator.Send(new GetAllFlightsQuery());
			return Ok(flights);
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateFlight([FromBody] CreateFlightCommand command)
		{
			var flight = await _mediator.Send(command);
			return CreatedAtAction(nameof(GetFlightById), new { id = flight.Id }, flight);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetFlightById([FromRoute] GetFlightByIdQuery id)
		{
			var flight = await _mediator.Send(id);
			return Ok(flight);
		}

    }
}
