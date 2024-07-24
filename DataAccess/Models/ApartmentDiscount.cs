
namespace DataAccess.Models
{
    public class ApartmentDiscount
    {
        public Guid ApartmentId { get; set; }
        public Guid DiscountId { get; set; }
        public DateOnly ExpiresAt { get; set; }
        public Apartment Apartment { get; set; }
        public Discount Discount { get; set; }
    }
}
