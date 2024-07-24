
namespace Presentation.Dependencies;

public static class AutoMapperExtensions
{
    public static void AddAutoMapperServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}