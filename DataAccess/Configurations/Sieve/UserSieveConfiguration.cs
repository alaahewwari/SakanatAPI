using DataAccess.Models;
using Sieve.Services;

namespace DataAccess.Configurations.Sieve;

public class UserSieveConfiguration : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<ApplicationUser>(a => a.Role.Name)
            .CanFilter();

        mapper.Property<ApplicationUser>(a => a.FirstName)
            .CanFilter()
            .CanSort();
        mapper.Property<ApplicationUser>(a => a.LastName)
            .CanFilter()
            .CanSort();
        mapper.Property<ApplicationUser>(a => a.Suspension)
            .CanFilter()
            .CanSort();
        mapper.Property<ApplicationUser>(a => a.Email)
            .CanFilter();

        mapper.Property<ApplicationUser>(a => a.CreationDate)
            .CanSort();
    }
}