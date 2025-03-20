using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBookingSystem.Domain.Entities
{
	public class Availability
	{
        public string flightId { get; set; }
        public int AvailableSeats { get; set; }
        public Dictionary<string,int> SeatType { get; set; }
        public Flight Flight { get; set; }
        public Availability()
        {
            SeatType = new Dictionary<string,int>();
        }

		public void UpdateAvailableSeats(int seats)
		{
			AvailableSeats = seats;
		}
		public void AddSeatType(string seatType, int numberOfSeats)
		{
			if (SeatType.ContainsKey(seatType))
			{
				SeatType[seatType] += numberOfSeats;
			}
            else
            {
                SeatType.Add(seatType, numberOfSeats);
            }
        }

		public int GetAvailableSeats(string  seatType)
		{
			if(SeatType.ContainsKey(seatType))
				return SeatType[seatType];
			else 
				throw new ArgumentException($"Seat type {seatType} does not exist");
		}

		public int GetTotalAvailableSeats()
		{
			return SeatType.Values.Sum();
		}
        public class AvailabilityBuilder
		{
			private readonly Availability _availability;

			public AvailabilityBuilder(Availability availability)
			{
				_availability = new Availability();
			}

			public AvailabilityBuilder SetFlightId(string flightId)
			{
				_availability.flightId = flightId;
				return this;
			}
			public AvailabilityBuilder SetAvailableSeats(int availableSeats)
			{
				_availability.UpdateAvailableSeats(availableSeats);
				return this;
			}
			public AvailabilityBuilder SetSeatType(string seatType, int numberOfSeats)
			{
				_availability.AddSeatType(seatType, numberOfSeats);
				return this;
			}
			
			public Availability Build()
			{
				return _availability;
			}
		}
	}
}
