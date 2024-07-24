using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Dependencies;

public static class DbContextExtensions
{
    public static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
    }
}