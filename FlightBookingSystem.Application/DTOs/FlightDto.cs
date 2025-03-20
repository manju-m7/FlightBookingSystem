﻿namespace FlightBookingSystem.Application.DTOs
{
	public class FlightDto
	{
        public Guid Id { get; set;}
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}
