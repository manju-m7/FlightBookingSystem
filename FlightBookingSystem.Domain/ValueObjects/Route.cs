namespace FlightBookingSystem.Domain.ValueObjects
{
	public class Route
	{
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Route()
        {
        }
        public Route(string origin, string destination)
        {
            if (string.IsNullOrEmpty(origin))
                throw new ArgumentNullException("Origin cannot be empty");
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException("Destination cannot be empty");
            Origin = origin;
            Destination = destination;
        }
		public override bool Equals(object? obj)
		{
			if (obj is not Route other) return false;
            return Origin == other.Origin && Destination == other.Destination;
		}
		public override int GetHashCode() => HashCode.Combine(Origin, Destination);
		
	}
}
