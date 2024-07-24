using DataAccess.Models;
using Sieve.Services;
namespace DataAccess.Configurations.Sieve
{
    public class ContractSieveConfiguration: ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Contract>(c => c.Id)
                .CanFilter()
                .CanSort();
            mapper.Property<Contract>(c => c.ApartmentId)
                .CanFilter()
                .CanSort();
            mapper.Property<Contract>(c => c.TenantId)
                .CanFilter()
                .CanSort();
            mapper.Property<Contract>(c => c.StartDate)
                .CanFilter()
                .CanSort();
            mapper.Property<Contract>(c => c.EndDate)
                .CanFilter()
                .CanSort();
            mapper.Property<Contract>(c => c.type)
                .CanFilter()
                .CanSort();
            mapper.Property<Contract>(c => c.IsTerminated)
                .CanFilter()
                .CanSort();
        }
    }
}
