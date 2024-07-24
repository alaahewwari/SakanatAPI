using DataAccess;
using Sieve.Models;
using Sieve.Services;

namespace Presentation.Dependencies;

public static class SieveConfiguration
{
    public static IServiceCollection AddSieve(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SieveOptions>(configuration.GetSection("Sieve"));
        services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
        return services;
    }
}