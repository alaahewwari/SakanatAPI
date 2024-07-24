namespace Presentation.Dependencies;
public static class CorsExtensions
{
    public static void AddCorsServices(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .WithOrigins(
                        "http://localhost:3000",
                        "https://sakanat-dev.vercel.app",
                        "https://sakanatdev1.vercel.app"
                    )
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination");
            });
        });
    }
}