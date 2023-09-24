namespace Simulator
{
    /// <summary>
    /// Generates flight data for simulation purposes.
    /// </summary>
    public class FlightGenerator
    {
        public object GenerateFlightDto()
        {
            return new
            {
                SerialNumber = GenerateRandomSerialNumber(),
                ArrivalTime = DateTime.Now,
                DepartureTime = DateTime.Now.AddSeconds(45),
            };
        }

        private string GenerateRandomSerialNumber()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }
    }
}
