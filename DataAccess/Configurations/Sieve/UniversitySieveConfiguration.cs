using DataAccess.Models;
using Sieve.Services;

namespace DataAccess.Configurations.Sieve;

public class UniversitySieveConfiguration : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<University>(u => u.Name)
            .CanFilter();
        mapper.Property<University>(u => u.CityContainer)
            .CanFilter();
    }
}