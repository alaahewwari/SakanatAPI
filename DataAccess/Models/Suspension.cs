namespace DataAccess.Models;

public class Suspension
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ApartmentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Reason { get; set; }
    public ApplicationUser SuspendedUser { get; set; }
    public Apartment Apartment { get; set; }

}