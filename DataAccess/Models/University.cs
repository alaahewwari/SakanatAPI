namespace DataAccess.Models;

public class University
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly CreationDate { get; set; }
    public ICollection<Apartment> Apartments { get; set; } = [];
    public ICollection<City> CityContainer { get; set; } = [];
}