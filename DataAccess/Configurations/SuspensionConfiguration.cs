using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class SuspensionConfiguration : IEntityTypeConfiguration<Suspension>
{
    public void Configure(EntityTypeBuilder<Suspension> builder)
    {
        builder
            .HasOne(s => s.Apartment)
            .WithOne(a => a.Suspension)
            .HasForeignKey<Suspension>(s => s.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}