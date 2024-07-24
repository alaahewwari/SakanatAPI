using BusinessLogic.Services.CleanupServices.Implementations;
using Microsoft.Extensions.Configuration;
using Presentation.Dependencies;

namespace Presentation.Extensions;

public static class ApplicationSetup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDbContext(configuration);
        services.AddSieve(configuration);
        services.AddApplicationServices(configuration);
        services.AddSwaggerServices(configuration);
        services.AddIdentityServices(configuration);
        services.AddAuthenticationServices(configuration);
        services.AddEmailServices(configuration);
        services.AddAutoMapperServices();
        services.AddCorsServices();
        services.AddFluentValidation(configuration);
        services.AddSignalR();
        services.AddCloudinaryServices(configuration);
        services.AddHostedService<SuspensionCleanupServices>();
        services.AddLogging();

    }
}