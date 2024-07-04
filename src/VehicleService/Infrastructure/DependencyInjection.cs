using Application.Base.Command;
using Application.Base.Query;
using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

        public static IServiceCollection AddCommandHandlers(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerInterfaceType in interfaces.Where(i =>
                             i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                {
                    services.AddScoped(handlerInterfaceType.AsType(), type.AsType());
                }
            }

            return services;
        }

        public static IServiceCollection AddQueryHandlers(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerInterfaceType in interfaces.Where(i =>
                             i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                {
                    services.AddScoped(handlerInterfaceType.AsType(), type.AsType());
                }
            }

            return services;
        }
    }
}
