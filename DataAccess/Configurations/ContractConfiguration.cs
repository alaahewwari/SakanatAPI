using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            var converter = new ValueConverter<decimal, double>(
                v => (double)v,
                v => (decimal)v);

            builder.Property(x => x.RentPrice).HasConversion(converter);

            builder.HasMany(p => p.PaymentLogs)
                .WithOne(p => p.Contract)
                .HasForeignKey(p => p.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ad => ad.Apartment)
                .WithMany(a => a.Contracts)
                .HasForeignKey(ad => ad.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Tenant)
                .WithMany(d => d.Contracts)
                .HasForeignKey(ad => ad.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
