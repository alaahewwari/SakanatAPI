using DataAccess.Enums.Apartment;
namespace DataAccess.Models;
public class Apartment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string Building { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool IsVisible { get; set; } = true;
    public string Description { get; set; }
    public int Price { get; set; }
    public DateOnly CreationDate { get; set; }
    public FurnishedStatus FurnishedStatus { get; set; }
    public GenderAllowed GenderAllowed { get; set; } 
    public PriceCurrency PriceCurrency { get; set; }
    public Guid CityId { get; set; }
    public Guid UniversityId { get; set; }
    public Guid UserId { get; set; }
    public City City { get; set; }
    public University NearbyUniversity { get; set; }
    public ApplicationUser User { get; set; }
    public Suspension? Suspension { get; set; }
    public ICollection<ApartmentDiscount> ApartmentDiscounts { get; set; } = [];
    public ICollection<ApartmentImage> ApartmentImages { get; set; } = [];
    public ICollection<ApplicationUser> FavoritedByUsers { get; set; } = [];
    public ICollection<Contract> Contracts { get; set; } = [];
}