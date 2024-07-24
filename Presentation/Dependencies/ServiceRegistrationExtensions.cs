using BusinessLogic.Repositories.Implementations;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.ApartmentServices.Implementations;
using BusinessLogic.Services.ApartmentServices.Interfaces;
using BusinessLogic.Services.AuthenticationServices.Implementations;
using BusinessLogic.Services.AuthenticationServices.Interfaces;
using BusinessLogic.Services.CityServices.Implementations;
using BusinessLogic.Services.CityServices.Interfaces;
using BusinessLogic.Services.ContractServices.Implementations;
using BusinessLogic.Services.ContractServices.Interfaces;
using BusinessLogic.Services.DiscountsServices.Implementations;
using BusinessLogic.Services.DiscountsServices.Interfaces;
using BusinessLogic.Services.EmailServices.Implementations;
using BusinessLogic.Services.EmailServices.Interfaces;
using BusinessLogic.Services.IdentityServices.Implementations;
using BusinessLogic.Services.IdentityServices.Interfaces;
using BusinessLogic.Services.NotificationServices.Implementations;
using BusinessLogic.Services.NotificationServices.Interfaces;
using BusinessLogic.Services.TenantServices.Implementations;
using BusinessLogic.Services.TenantServices.Interfaces;
using BusinessLogic.Services.UniversityServices.Implementations;
using BusinessLogic.Services.UniversityServices.Interfaces;
using BusinessLogic.Services.UserFollowingServices.Implementations;
using BusinessLogic.Services.UserFollowingServices.Interfaces;
using BusinessLogic.Services.UserServices.Implementations;
using BusinessLogic.Services.UserServices.Interfaces;

namespace Presentation.Dependencies;
public static class ServiceRegistrationExtensions
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IRegistrationServices, RegistrationServices>();
        services.AddScoped<ICityServices, CityServices>();
        services.AddScoped<IUniversityServices, UniversityServices>();
        services.AddScoped<ILoginServices, LoginServices>();
        services.AddScoped<IEmailServices, EmailServices>();
        services.AddScoped<ITokenServices, TokenServices>();
        services.AddScoped<IPasswordServices, PasswordServices>();
        services.AddScoped<IApartmentServices, ApartmentServices>();
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IIdentityManager, IdentityManager>();
        services.AddScoped<ISuspensionServices, SuspensionServices>();
        services.AddScoped<IFavouritesServices, FavouritesServices>();
        services.AddScoped<IUserFollowingServices, UserFollowingServices>();
        services.AddScoped<IIdentityManager, IdentityManager>();
        services.AddScoped<ITenantServices, TenantServices>();
        services.AddScoped<IDiscountServices,DiscountServices>();
        services.AddScoped<INotificationServices, NotificationServices>();
        services.AddScoped<IAuthenticationServicesFacade, AuthenticationServicesFacade>();
        services.AddScoped<IContractServices, ContractServices>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICityRepository,CityRepository>();
        services.AddScoped<IUniversityRepository, UniversityRepository>();
        services.AddScoped<IApartmentRepository, ApartmentRepository>();
        services.AddScoped<ISuspensionRepository, SuspensionRepository>();
        services.AddScoped<IFavouritesRepository, FavouritesRepository>();
        services.AddScoped<IFollowingRepository, FollowingRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IContractRepository, ContractRepository>();
    }
}