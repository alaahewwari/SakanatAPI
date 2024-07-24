using DataAccess.Models;
using Sieve.Services;
namespace DataAccess.Configurations.Sieve
{
    public class ApartmentSieveConfiguration :ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Apartment>(a => a.Price)
                .CanFilter()
                .CanSort();

            mapper.Property<Apartment>(a => a.City.Name)
                .CanFilter();

            mapper.Property<Apartment>(a => a.NearbyUniversity.Name)
                .CanFilter();

            mapper.Property<Apartment>(a => a.FurnishedStatus)
                .CanFilter();

            mapper.Property<Apartment>(a => a.GenderAllowed)
                .CanFilter();

            mapper.Property<Apartment>(a => a.NumberOfRooms)
                .CanFilter();

            mapper.Property<Apartment>(a => a.NumberOfBathrooms)
                .CanFilter();

            mapper.Property<Apartment>(a => a.PriceCurrency)
                .CanFilter();

            mapper.Property<Apartment>(a => a.IsAvailable)
                .CanFilter();

            mapper.Property<Apartment>(a => a.IsVisible)
                .CanFilter();

            mapper.Property<Apartment>(a => a.CreationDate)
                .CanFilter()
                .CanSort();
            mapper.Property<Apartment>(a => a.ApartmentDiscounts.Count)
                .CanFilter()
                .CanSort();
            mapper.Property<Apartment>(a=>a.FavoritedByUsers.Count)
                .CanFilter()
                .CanSort();
            mapper.Property<Apartment>(a => a.ApartmentImages.Count)
                .CanFilter()
                .CanSort();
            mapper.Property<Apartment> (a=>a.GenderAllowed)
                .CanFilter();
        }
    }
}
