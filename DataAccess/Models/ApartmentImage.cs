
namespace DataAccess.Models;

public class ApartmentImage
{
    public Guid Id { get; set; }
    public string ImagePath { get; set; }
    public bool IsCover { get; set; }=false;
    public Guid ApartmentId { get; set; }
    public Apartment Apartment { get; set; }=null!;
}