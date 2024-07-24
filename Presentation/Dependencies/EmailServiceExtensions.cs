using BusinessLogic.Infrastructure.Email;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Dependencies;
public static class EmailServiceExtensions
{
    public static void AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        services.AddSingleton(emailConfig!);

        services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
        services.Configure<IdentityOptions>(options => { options.SignIn.RequireConfirmedEmail = true; });

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(10);
        });
    }
}