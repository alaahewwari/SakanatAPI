using DataAccess.Models;
using Sieve.Services;

namespace DataAccess.Configurations.Sieve
{
    public class DiscountSieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Discount>(d =>d.Percentage)
                .CanFilter()
                .CanSort();
            mapper.Property<Discount>(d =>d.ApartmentDiscounts.Count)
                .CanFilter()
                .CanSort();
        }
    }
}