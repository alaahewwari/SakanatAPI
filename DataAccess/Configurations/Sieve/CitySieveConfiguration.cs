using DataAccess.Models;
using Sieve.Services;

namespace DataAccess.Configurations.Sieve;

public class CitySieveConfiguration : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<City>(c => c.Name)
            .CanFilter()
            .CanSort();
        mapper.Property<City>(c => c.CreationDate)
            .CanFilter()
            .CanSort();

        
    }
}