using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasMany(c => c.ContainedUniversities)
            .WithMany(u => u.CityContainer)
            .UsingEntity<Dictionary<string, object>>(
                "CityUniversities",
                i => i.HasOne<University>()
                    .WithMany()
                    .HasForeignKey("NearbyUniversityId")
                    .HasPrincipalKey("Id")
                    .OnDelete(DeleteBehavior.NoAction),
                i => i.HasOne<City>()
                    .WithMany()
                    .HasForeignKey("CityId")
                    .HasPrincipalKey("Id")
                    .OnDelete(DeleteBehavior.NoAction),
                i => i.HasKey("CityId", "NearbyUniversityId"));

        //builder.HasMany(t=>t.Tenants)
        //    .WithOne(c=>c.City)
        //    .HasForeignKey(c=>c.CityId)
        //    .OnDelete(DeleteBehavior.Restrict);


    }
}