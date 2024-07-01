using Domain.Interface;
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
            services.AddDbContext<VehicleContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IEngineRepository, EngineRepository>();

            return services;
        }
    }
}
