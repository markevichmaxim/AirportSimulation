using API.Host;

namespace API.Configuration
{
    public static class HostedServices
    {
        public static IServiceCollection RegisterHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<AirportDataStartupHandler>();
            
            return services;
        }
    }
}
