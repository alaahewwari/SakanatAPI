

namespace DataAccess.Models
{
    public class PaymentLog
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
