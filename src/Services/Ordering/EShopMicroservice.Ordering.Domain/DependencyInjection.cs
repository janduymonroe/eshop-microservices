using Microsoft.Extensions.DependencyInjection;

namespace EShopMicroservice.Ordering.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services;
    }
}
