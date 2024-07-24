using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Configurations;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasMany(u=>u.RefreshTokens)
            .WithOne(r=>r.User)
            .HasForeignKey(r=>r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.City)
            .WithMany(d => d.Users)
            .HasForeignKey(u => u.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Apartments)
            .WithOne(u => u.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Suspension)
            .WithOne(s => s.SuspendedUser)
            .HasForeignKey<Suspension>(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u=>u.Discounts)
            .WithOne(d=>d!.User)
            .HasForeignKey(d=>d!.UserId)
            .OnDelete(DeleteBehavior.Restrict);


        builder
            .HasMany(f => f.FavouriteApartments)
            .WithMany(a => a.FavoritedByUsers)
            .UsingEntity("FavouriteApartment",
                i => i.HasOne(typeof(Apartment)).WithMany().HasForeignKey("ApartmentId").OnDelete(DeleteBehavior.Restrict),
                i => i.HasOne(typeof(ApplicationUser)).WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict),
                i => i.HasKey("ApartmentId", "UserId"));

        builder
            .HasMany(t => t.Tenants)
            .WithOne(t => t.Owner)
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}