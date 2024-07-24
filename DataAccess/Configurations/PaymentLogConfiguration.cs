

using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Configurations
{
    public class PaymentLogConfiguration : IEntityTypeConfiguration<PaymentLog>
    {
        public void Configure(EntityTypeBuilder<PaymentLog> builder)
        {

            var converter = new ValueConverter<decimal, double>(
                v => (double)v,
                v => (decimal)v);

            builder.Property(x => x.Amount).HasConversion(converter);

            builder.HasOne(p => p.Contract)
                               .WithMany(c => c.PaymentLogs)
                                              .HasForeignKey(p => p.ContractId)
                                                             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
