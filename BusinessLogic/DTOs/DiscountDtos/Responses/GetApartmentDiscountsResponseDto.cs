
namespace BusinessLogic.DTOs.DiscountDtos.Responses
{
    public record GetApartmentDiscountsResponseDto
    {
        public Guid Id { get; init; }
        public string Description { get; init; }
        public double Percentage { get; init; }
        public DateOnly ExpiryDate { get; init; }
    }
}
