using BusinessLogic.DTOs.EnumDtos;
using BusinessLogic.DTOs.PaymentDtos.Responses;

namespace BusinessLogic.DTOs.ContractDtos.Responses
{
    public class ContractResponseDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal RentPrice { get; set; }
        public PriceCurrencyDto Currency { get; set; }
        public string TenantName { get; set; }
        public bool IsTerminated { get; set; }
        public IList<PaymentResponseDto> PaymentLogs { get; set; } = new List<PaymentResponseDto>();
        public decimal TotalPayments { get; set; }
        public decimal RemainingPayments { get; set; }
        public Guid ApartmentId { get; set; }
    }
}
