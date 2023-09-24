using Simulator;

ClientSimulator Client = new ClientSimulator();
FlightGenerator FlightGenerator = new FlightGenerator();

while (true)
{
    await Client.SendFlight(FlightGenerator.GenerateFlightDto());
    await Task.Delay(TimeSpan.FromSeconds(13));
}