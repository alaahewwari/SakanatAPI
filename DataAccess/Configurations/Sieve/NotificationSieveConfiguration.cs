using DataAccess.Models;
using Sieve.Services;

namespace DataAccess.Configurations.Sieve
{
    internal class NotificationSieveConfiguration : ISieveConfiguration

    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Notification>(n => n.CreationDate)
                .CanFilter()
                .CanSort();
            mapper.Property<Notification>(n => n.Status)
                .CanFilter()
                .CanSort();
            mapper.Property<Notification>(n => n.Type)
                .CanFilter()
                .CanSort();
        }
    }
}
