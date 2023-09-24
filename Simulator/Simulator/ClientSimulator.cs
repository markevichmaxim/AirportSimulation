using System.Text;
using System.Text.Json;

namespace Simulator
{
    /// <summary>
    /// Simulates a client to send flight data to a server using HTTP POST requests.
    /// </summary>
    public class ClientSimulator
    {
        private readonly string URL = "https://localhost:7296/airport/send-flight";

        public async Task SendFlight(object flightDto)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpContent content = new StringContent(JsonSerializer.Serialize(flightDto), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(URL, content);

                    if (response.IsSuccessStatusCode)
                        Console.WriteLine($"Flight: {JsonSerializer.Serialize(flightDto)} has been sent to the server");
                    else
                        Console.WriteLine($"Error: {response.StatusCode}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
