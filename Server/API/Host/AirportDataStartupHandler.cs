using Core.Interfaces.Services;
using Service.Airport.Processing;

namespace API.Host
{
    /// <summary>
    /// Background service responsible for initializing airport data processing on startup from the database.
    /// </summary>
    public class AirportDataStartupHandler : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly FacilityProcesses _startupHandler;

        public AirportDataStartupHandler(IServiceScopeFactory scopeFactory, FacilityProcesses startupHandler)
        {
            _scopeFactory = scopeFactory;
            _startupHandler = startupHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var stateDataPicker = scope.ServiceProvider.GetRequiredService<IStateDataLoader>();

            var isRunwayLoaded = await stateDataPicker.LoadRunwayState();
            var areGatewaysLoaded = await stateDataPicker.LoadTerminalState();
            var areQueuesLoaded = await stateDataPicker.LoadQueuesState();

            if (isRunwayLoaded)
                await Task.Run(_startupHandler.ProcessRunway);

            if (areGatewaysLoaded.Values.Any())
            {
                foreach (var gateway in areGatewaysLoaded)
                {
                    if (gateway.Value)
                        await Task.Run(() => _startupHandler.ProcessTerminal(gateway.Key));
                }
            }

            if (areQueuesLoaded.Values.Any(valueTrue => valueTrue))
                _startupHandler.ProcessQueues(areQueuesLoaded);
        }
    }
}
