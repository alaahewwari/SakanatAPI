using Presentation.Extensions;

namespace Presentation.Dependencies;

public static class ApplicationServicesExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBusinessServices();
        services.AddRepositories();
    }
}