using DataAccess.Enums.Apartment;
namespace DataAccess.Models
{
    public class Contract
    {
        public Guid Id { get; set; }
        public RentPeriod type { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal RentPrice { get; set; }
        public PriceCurrency Currency { get; set; }
        public bool IsTerminated { get; set; } = false;
        public Guid TenantId { get; set; }
        public Guid ApartmentId { get; set; }
        public Tenant Tenant { get; set; }
        public Apartment Apartment { get; set; }
        public ICollection<PaymentLog> PaymentLogs { get; set; } = [];
    }
}
