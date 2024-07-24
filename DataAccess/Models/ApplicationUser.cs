using Microsoft.AspNetCore.Identity;
namespace DataAccess.Models;
public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ImagePath { get; set; }
    public DateOnly CreationDate { get; set; }
    public Guid CityId { get; set; }
    public Guid RoleId { get; set; }
    public City City { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public Suspension? Suspension { get; set; }
    public ICollection<Apartment> Apartments { get; set; } = [];
    public ICollection<UserFollows> FollowingUsers { get; set; } = [];
    public ICollection<UserFollows> FollowerUsers { get; set; } = [];
    public ICollection<Notification> SentNotifications { get; set; } = [];
    public ICollection<Notification> Notifications { get; set; } = [];
    public ICollection<Apartment> FavouriteApartments { get; set; } = [];
    public ICollection<Tenant> Tenants { get; set; } = [];
    public ICollection<Discount> Discounts { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}