using FluentValidation.AspNetCore;
using Presentation.Common.Validation;
namespace Presentation.Dependencies;
public static class FluentValidationExtensions 
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services, IConfiguration configuration)
    {
        // Registers all validators that implement the IValidatorMarker interface across all assemblies
        _ = services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IValidatorMarker>());
        return services;
    }
}