using Microsoft.Extensions.DependencyInjection;

namespace Project.Infrastructure.Dependencies;

public static class DataSeederDependency
{
    public static IServiceCollection AddDataSeeder(this IServiceCollection services)
    {
        // services.AddScoped<IdentitiesDataSeeder>();

        return services;
    }
}
