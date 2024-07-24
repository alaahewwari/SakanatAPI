namespace DataAccess.Models;

public class City
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly CreationDate { get; set; }
    public ICollection<ApplicationUser> Users { get; set; } = [];
    public ICollection<Apartment> Apartments { get; set; } = [];
    public ICollection<University> ContainedUniversities { get; set; } = [];
}