using DataAccess.Models;
using Sieve.Services;


namespace DataAccess.Configurations.Sieve;

public class SuspensionSieveConfiguration : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Suspension>(s => s.EndDate)
            .CanFilter()
            .CanSort();
        
    }
}