using Domain.Interface;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<ParkingContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IParkingPlaceRepository, ParkingPlaceRepository>();
            services.AddScoped<IParkingRepository, ParkingRepository>();

            return services;
        }
    }
}
