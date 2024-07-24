using BusinessLogic.Infrastructure.Cloudinary;
using BusinessLogic.Services.StorageServices.Implementations;
using BusinessLogic.Services.StorageServices.Interfaces;

namespace Presentation.Dependencies
{
    public static class CloudinaryExtensions
    {
        public static void AddCloudinaryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            services.AddScoped<ICloudinaryServices, CloudinaryServices>();
        }
    }
}
