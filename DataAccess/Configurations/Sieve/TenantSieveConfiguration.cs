using DataAccess.Models;
using Sieve.Services;

namespace DataAccess.Configurations.Sieve
{
    public class TenantSieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Tenant>(a => a.FirstName)
                .CanFilter()
                .CanSort();
            mapper.Property<Tenant>(a => a.LastName)
                .CanFilter()
                .CanSort();
            mapper.Property<Tenant>(a => a.CreationDate)
                .CanFilter()
                .CanSort();
            mapper.Property<Tenant>(a => a.City)
                .CanFilter();

            mapper.Property<Tenant>(a => a.Contracts)
                .CanFilter();
        }
    }
}
