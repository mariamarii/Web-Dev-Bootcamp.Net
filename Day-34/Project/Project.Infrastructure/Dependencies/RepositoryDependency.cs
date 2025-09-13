using Microsoft.Extensions.DependencyInjection;
using Project.Application.Abstractions.Repositories;
using Project.Infrastructure.Repositories;

namespace Project.Infrastructure.Dependencies;

public static class RepositoryDependency
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

        // services.AddScoped<IUserCodeRepository, UserCodeRepository>();


        return services;
    }
}
