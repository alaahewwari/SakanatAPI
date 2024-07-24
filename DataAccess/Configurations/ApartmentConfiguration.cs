using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.HasKey(a => a.Id);


        builder.HasOne(a => a.City)
            .WithMany(c => c.Apartments)
            .HasForeignKey(a => a.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.NearbyUniversity)
            .WithMany(n => n.Apartments)
            .HasForeignKey(a => a.UniversityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Suspension)
            .WithOne(a => a.Apartment)
            .HasForeignKey<Suspension>(s => s.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.ApartmentImages)
            .WithOne(a => a.Apartment)
            .HasForeignKey(a => a.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}