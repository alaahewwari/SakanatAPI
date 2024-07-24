namespace DataAccess.Models;

public class Discount
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public double Percentage { get; set; }
    public DateOnly CreationDate { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<ApartmentDiscount> ApartmentDiscounts { get; set; } = [];

}