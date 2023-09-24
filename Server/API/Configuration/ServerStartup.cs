using API.Hubs;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using Service.Airport.Data;

namespace API.Configuration
{
    /// <summary>
    /// Configures the server at startup to subscribe to airport event-driven functionality and SignalR communication
    /// </summary>
    public static class ServerStartup
    {
        public static WebApplication SetupAirportEventDrivenEngine(this WebApplication app)
        {
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                var settingStateService = app.Services.GetRequiredService<IEventDrivenEngine>();

                settingStateService.SubscribeToEventHandlers();
            });

            return app;
        }

        public static WebApplication SetupSignalR(this WebApplication app)
        {
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                var dataEvents = app.Services.GetRequiredService<DataEvents>();
                var hubContext = app.Services.GetRequiredService<IHubContext<AirportHub>>();

                dataEvents.DataUpdated += async updatedData => 
                    await hubContext.Clients.All.SendAsync("ReceiveUpdatedData", updatedData);
            });

            return app;
        }
    }
}
