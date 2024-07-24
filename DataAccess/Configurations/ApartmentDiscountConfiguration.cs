using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ApartmentDiscountConfiguration : IEntityTypeConfiguration<ApartmentDiscount>
    {
        public void Configure(EntityTypeBuilder<ApartmentDiscount> builder)
        {
            builder.HasKey(ad => new
            {
                ad.ApartmentId,
                ad.DiscountId
            });

            builder.HasOne(ad => ad.Apartment)
            .WithMany(a => a.ApartmentDiscounts)
                .HasForeignKey(ad => ad.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ad => ad.Discount)
            .WithMany(d => d.ApartmentDiscounts)
                .HasForeignKey(ad => ad.DiscountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
}
}
