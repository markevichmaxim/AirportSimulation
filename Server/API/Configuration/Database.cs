using DAL;
using Microsoft.EntityFrameworkCore;

namespace API.Configuration
{
    /// <summary>
    /// Creating the database or using an in-memory database if no connection string is provided in "appsettings.json".
    /// </summary>
    public static class Database
    {
        public static IServiceCollection ConnectToDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("AirportDb");
            var isDatabaseAvailable = !string.IsNullOrWhiteSpace(dbConnectionString);

            services.AddDbContext<AirportDbContext>(options =>
            {
                if (isDatabaseAvailable)
                    options.UseSqlServer(dbConnectionString);
                else
                    options.UseInMemoryDatabase("AirportInMemoryDb");
            });

            if (isDatabaseAvailable)
            {
                var serviceProvider = services.BuildServiceProvider();
                var dbContext = serviceProvider.GetRequiredService<AirportDbContext>();
                dbContext.Database.Migrate();
            }

            return services;
        }
    }
}
