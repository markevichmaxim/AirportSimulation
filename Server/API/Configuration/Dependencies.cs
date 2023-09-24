using API.Models.Dto;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using DAL.Repositories;
using Service.Airport.Configuration;
using Service.Airport.Data;
using Service.Airport.Processing;
using Service.Airport.State;

namespace API.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterCustomDependencies(this IServiceCollection services)
        {
            services.AddSingleton<AirportState>();
            services.AddSingleton<StateEvents>();

            services.AddSingleton<DataEvents>();

            services.AddScoped<IFacilityRepository, FacilityRepository>();

            services.AddTransient<IFlightRepository, FlightRepository>();

            services.AddTransient<StateManager>();
            services.AddTransient<AirportManager>();
            services.AddTransient<FacilityProcesses>();

            services.AddTransient<IEventDrivenEngine, EventDrivenEngine>();
            services.AddTransient<IStateDataLoader, StateDataLoader>();
            services.AddTransient<IStateDataSender, StateDataSender>();

            services.AddTransient<DtoMapper>();

            return services;
        }
    }
}
