using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Infrastructure.Data;
using Project.Infrastructure.Interceptors;

namespace Project.Infrastructure.Dependencies;

public static class DatabaseDependency
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        // services.AddScoped<AuditInterceptor>();
        services.AddScoped<SoftDeleteInterceptor>();
        // services.AddScoped<NotificationInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString, sqlServerOptions =>
            {
                sqlServerOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName);
                sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }).UseSnakeCaseNamingConvention();

            options.AddInterceptors(
                // sp.GetRequiredService<AuditInterceptor>(),
                sp.GetRequiredService<SoftDeleteInterceptor>()
                // sp.GetRequiredService<NotificationInterceptor>()
            );
        });


        AppContext.SetSwitch("Microsoft.AspNetCore.Identity.CheckPasswordSignInAlwaysResetLockoutOnSuccess", true);

        //
        // services.AddIdentity<User, Role>(options =>
        //     {
        //         options.User.RequireUniqueEmail = true;
        //         options.SignIn.RequireConfirmedEmail = true;
        //         options.SignIn.RequireConfirmedAccount = true;
        //         options.Password.RequireDigit = true;
        //         options.Password.RequiredLength = 8;
        //         options.Password.RequireLowercase = true;
        //         options.Password.RequireUppercase = true;
        //         options.Password.RequireNonAlphanumeric = true;
        //         options.Lockout.AllowedForNewUsers = false;
        //         options.Lockout.MaxFailedAccessAttempts = 1;
        //         options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //     })
        //     .AddEntityFrameworkStores<ApplicationDbContext>()
        //     .AddDefaultTokenProviders();
        
        return services;
    }
}
