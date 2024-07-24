

namespace BusinessLogic.DTOs.PaymentDtos.Responses
{
    public class PaymentResponseDto
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
    }
}
