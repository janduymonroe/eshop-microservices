using BuildingBlocks.Messaging.MassTransit;
using BuldingBlocks.Behaviours;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace EShopMicroservice.Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });

        services.AddFeatureManagement();
        services.AddMessageBroker(configuration, assembly);

        return services;
    }
}
