using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Configurations;
public static class IdentityTablesConfiguration
{
    public static void CustomizeIdentityTablesNames(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        modelBuilder.Entity<Role>().ToTable("Roles");
    }
}